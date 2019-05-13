using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace UniRxExamples
{
    public class MouseDoubleClick : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        private void Start()
        {
            var clickStream = this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0));

            clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(200)))
                .Where(x => x.Count >= 2)
                .SubscribeToText(_text, x =>
                    $"DoubleClick detected!\n Count:{x.Count}");
        }
    }
}