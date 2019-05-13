﻿using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace UniRxExamples
{
    public class ThrottleIsGrounded : MonoBehaviour
    {
        [SerializeField]
        private Text throttledValueText;

        [SerializeField]
        private Text rawValuesText;

        [SerializeField]
        private CharacterController playerCharacterController;

        private bool throttledIsGrounded;

        public void Start()
        {
            //IsGroundedが直近5フレーム以内で変動した場合は無視する
            //UpdateAsObservable()
            //    .Select(_ => playerCharacterController.isGrounded)
            //    .DistinctUntilChanged()
            //    .ThrottleFrame(5)
            //    .Subscribe(x => throttledIsGrounded = x);

            //毎フレーム変動を監視するならObserveEveryValueChangedが使える
            playerCharacterController
                .ObserveEveryValueChanged(x => x.isGrounded)
                .ThrottleFrame(5)
                .Subscribe(x => throttledIsGrounded = x);

            //IsGroundedの生の値を過去２０フレーム分まとめて表示する
            this.UpdateAsObservable()
                .Select(_ => playerCharacterController.isGrounded)
                .Select(x => x.ToString() + "\n")
                .Buffer(20, 1)
                .SubscribeToText(rawValuesText, x => x.Aggregate((p, n) => p + n));

            //Throttleを通した後のIsGroundedの値を過去２０フレーム分まとめて表示する
            this.UpdateAsObservable()
                .Select(_ => throttledIsGrounded)
                .Select(x => x.ToString() + "\n")
                .Buffer(20, 1)
                .SubscribeToText(throttledValueText, x => x.Aggregate((p, n) => p + n));
        }
    }
}