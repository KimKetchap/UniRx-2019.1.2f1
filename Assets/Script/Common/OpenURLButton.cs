using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UniRxExamples
{
    public class OpenURLButton : MonoBehaviour
    {

        private string baseUrl 
            = "https://github.com/TORISOUP/UniRxExamples/blob/master/Assets/Script/Examples";


        public void Start()
        {
            var levelName = SceneManager.GetActiveScene().ToString();
            var targetURL = string.Format("{0}/{1}/{1}.cs", baseUrl, levelName);


            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
#if UNITY_WEBPLAYER
                    var cmd = string.Format(("window.open('{0}','{1}')"),targetURL,levelName);
                    Application.ExternalEval(cmd);
#else
                    Application.OpenURL(targetURL);
#endif
                });
        }
    }
}