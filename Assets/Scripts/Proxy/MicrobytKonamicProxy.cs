using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using microbytkonamic.proxy;
using System;

namespace microbytkonamic.proxy
{
    public class MicrobytKonamicProxy : MonoBehaviour
    {
        public System.Uri urlLocal = new System.Uri("https://localhost:7076");
        public System.Uri urlServidor = new System.Uri("https://microbykonamic");
        public bool applyUrlLocalInEditor = true;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //StartCoroutine(GetFelicitacion());
        }

        IEnumerator GetFelicitacion(IntegerIntervals intervals, Func<UnityWebRequest, IEnumerator> callBack)
        {
            using (var webRequest = UnityWebRequest.Get("https://localhost:7076/api/postales/getfelicitacion"))
            {
                yield return webRequest.SendWebRequest();

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError("GetFelicitacion Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError("GetFelicitacion HTTP Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.Success:
                        Debug.Log("GetFelicitacion: \nReceived: " + webRequest.downloadHandler.text);
                        break;
                }

                if (callBack != null)
                    yield return StartCoroutine(callBack.Invoke(webRequest));
            }
        }

        private System.Uri GetUrlBase()
        {
            bool local = Application.isPlaying ? applyUrlLocalInEditor : false;

            return local ? urlLocal : urlServidor;
        }
    }
}
