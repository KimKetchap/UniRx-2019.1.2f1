using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace UniRxExamples
{
    public class MouseDrag : MonoBehaviour
    {
        private float _rotationSpeed = 500.0f;

        private void Start()
        {
            //OnMouseDownとOnMouseUpの組み合わせでドラッグ中のみ処理をする
            //(OnMouseDragを使えばいいじゃんって野暮なツッコミは無しで…)

            this.UpdateAsObservable()                      //Update()のタイミングを通知するObservable
                .SkipUntil(this.OnMouseDownAsObservable()) //マウスがクリックされるまでストリームを無視
                .Select(_ =>                          //マウスの移動量をストリームに流す
                    new Vector2(Input.GetAxis("Mouse X"),
                                Input.GetAxis("Mouse Y")))
                .TakeUntil(this.OnMouseUpAsObservable())   //マウスが離されるまで
                .Repeat()                             //TakeUntilでストリームが終了するので再Subscribe
                .Subscribe(move =>
                {
                    //オブジェクトをドラッグするとそのオブジェクトを回転させる
                    transform.rotation =
                        Quaternion.AngleAxis(move.y * _rotationSpeed * Time.deltaTime, Vector3.right) *
                        Quaternion.AngleAxis(-move.x * _rotationSpeed * Time.deltaTime, Vector3.up) *
                        transform.rotation;
                    ;
                });
        }
    }
}