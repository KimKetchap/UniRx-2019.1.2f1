using System;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace UniRxExamples
{
    public class ObservableWWWExample : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        [SerializeField]
        private Image _image;

        [SerializeField] private Text _errorText;

        private readonly string resourceURL
            = "http://torisoup.net/unirx-examples/resources/sampletexture.png";

        public void Start()
        {
            _button.OnClickAsObservable()
                .First()
                .SelectMany(ObservableWWW.GetWWW(resourceURL).Timeout(TimeSpan.FromSeconds(3)))
//                .SelectMany(UnityWebRequest.)
                .Select(www => Sprite.Create(www.texture, new Rect(0, 0, 400, 400), Vector2.zero))
                .Subscribe(sprite =>
                {
                    _image.sprite = sprite;
                }, ex => _errorText.text = ex.ToString());
            
            
        }
    }
}