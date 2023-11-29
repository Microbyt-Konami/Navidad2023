using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using microbytkonamic.proxy;
using System;
using System.Linq;

namespace microbytkonamic.proxy
{
    public class MicrobytKonamicProxy : MonoBehaviour
    {
        public string urlLocal = "https://localhost:7076";
        public string urlServidor = "https://microbykonamic";
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

        IEnumerator GetFelicitacion(GetFelicitacionIn intervals, Func<UnityWebRequest, IEnumerator> callBack)
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

        private string GetUrlBase()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
                return "";

            bool local = Application.isPlaying ? applyUrlLocalInEditor : false;
            string url = local ? urlLocal : urlServidor;

            if (url.LastOrDefault() == '/')
                url = url.Substring(0, url.Length - 1);

            return url;
        }

        private string GetApiUrl(string controller) => $"{GetUrlBase()}/api/{controller}";
        private string GetApiUrl(string controller, string method) => $"{GetApiUrl(controller)}/{method}";
        private UnityWebRequest Post(string controller, string method, string postData, string contentType) => UnityWebRequest.Post(GetApiUrl(controller, method), postData, contentType);
        private UnityWebRequest Post<T>(string controller, string method, T postData) //where T:class
        {
            var _postData = JsonUtility.ToJson(postData);
            var result = Post(controller, method, _postData);

            return result;
        }
        private IEnumerator PostCoroutine<TData, TResult>(string controller, string method, TData postData, Func<Exception, TResult, IEnumerator> callBack) //where T:class
        {
            using (var webRequest = Post(controller, method, postData))
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
                        yield return StartCoroutine(callBack.Invoke(null, JsonUtility.FromJson<TResult>(webRequest.downloadHandler.text)));
                        break;
                }
            }
        }
    }
}
