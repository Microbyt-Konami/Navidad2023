using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using microbytkonamic.proxy;
using System.Linq;

namespace microbytkonamic.proxy
{
    public class MicrobytKonamicProxy : MonoBehaviour
    {
        public string urlLocal = "https://localhost:7076";
        // Cuando cambiemos a https hay que cambiar roject settings -->Player --> Other settings --> Allow downloads over HTTP
        public string urlServidor = "http://microbykonamic.es";
        public bool applyUrlLocalInEditor = true;

        //private IEnumerator PostCoroutine<TData, TResult>(string controller, string method, TData postData, System.Func<System.Exception, TResult, IEnumerator> callBack)
        public IEnumerator GetFelicitacion(GetFelicitacionIn input, System.Func<System.Exception, GetFelicitacionResult, IEnumerator> callBack)
            => PostCoroutine("postales", "getfelicitacion", input, callBack);

        public IEnumerator AltaFelicitacion(AltaFelicitacionIn input, System.Func<System.Exception, IntegerIntervals, IEnumerator> callBack)
            => PostCoroutine("postales", "altafelicitacion", input, callBack);

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //StartCoroutine(GetFelicitacion());
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
            var result = Post(controller, method, _postData, "application/json");

            return result;
        }
        private IEnumerator PostCoroutine<TData, TResult>(string controller, string method, TData postData, System.Func<System.Exception, TResult, IEnumerator> callBack)
        {
            string msg;
            TResult result;
            System.Exception ex;

            using (var webRequest = Post(controller, method, postData))
            {
                yield return webRequest.SendWebRequest();

                string text = webRequest.downloadHandler.text;

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                    case UnityWebRequest.Result.ProtocolError:
                        msg = $"{GetApiUrl(controller, method)} postData: {JsonUtility.ToJson(postData)} Error: {webRequest.error}";
                        Debug.LogError(msg);

                        if (!string.IsNullOrWhiteSpace(text) && WebApiProblemDetails.TryParseFromJson(text, out var problemDetails))
                        {
                            ex = new WebApiProblemDetailsExceptions(problemDetails);
                            Debug.LogError(ex);
                        }
                        else
                            ex = new WebApiProblemDetailsExceptions(!string.IsNullOrWhiteSpace(text) ? text : webRequest.error);

                        yield return StartCoroutine(callBack.Invoke(ex, default(TResult)));
                        break;
                    case UnityWebRequest.Result.Success:
                        msg = $"{GetApiUrl(controller, method)} postData: {JsonUtility.ToJson(postData)} responseCode: {webRequest.responseCode} Received: {text}";
                        result = JsonUtility.FromJson<TResult>(text);
                        Debug.Log(msg);
                        yield return StartCoroutine(callBack.Invoke(null, result));
                        break;
                    default:
                        msg = $"{GetApiUrl(controller, method)} postData: {JsonUtility.ToJson(postData)} Result no controller: {webRequest.result}";
                        Debug.LogError(msg);
                        yield return StartCoroutine(callBack.Invoke(null, default(TResult)));
                        break;
                }
            }
        }
    }
}
