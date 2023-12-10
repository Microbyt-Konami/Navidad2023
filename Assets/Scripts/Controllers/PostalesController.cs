using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using microbytkonamic.proxy;

namespace microbytkonamic.navidad
{
    public class PostalesController : MonoBehaviourSingleton<PostalesController>
    {
        public int anyo;
        public AudioClip soundButton;
        public AudioClip soundError;
        private AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();
            if (isInstanceAsigned)
                audioSource = GetComponent<AudioSource>();
        }

        // Start is called before the first frame update
        void Start()
        {
            var altaFelicitacionController = FindAnyObjectByType<AltaFelicitacionController>();

            if (altaFelicitacionController != null)
            {
                altaFelicitacionController.ShowButtons();
                FelicitacionController.Instance.gameObject.SetActive(false);
            }
#if !UNITY_EDITOR && UNITY_WEBGL
                /*
                    Typically, introducing HTML elements (such as text fields) can cause errors if included in the web page that’s meant to receive keyboard inputs. 
                    Unity consumes the input events before the rest of the page can receive them.
                    To make HTML elements receive a keyboard input, set WebGLInput.captureAllKeyboardInput to false. This way, the application receives input only if the WebGL canvas has focus
                 */
                // disable WebGLInput.captureAllKeyboardInput so elements in web page can handle keyboard inputs
                WebGLInput.captureAllKeyboardInput = false;
#endif
        }

        public void PlaySoundButton()
        {
            audioSource.clip = soundButton;
            audioSource.Play();
        }

        public void PlaySoundError()
        {
            audioSource.clip = soundError;
            audioSource.Play();
        }

        public IEnumerator LoadScenePostalCoroutine(FelicitacionDto felicitacionDto = null, IntegerIntervals intervals = null)
        {
            var sceneAct = SceneManager.GetActiveScene();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            FelicitacionController.Instance.gameObject.SetActive(true);

            if (felicitacionDto != null && intervals != null)
                FelicitacionController.Instance.StartFelicitacion(felicitacionDto, intervals);

            asyncLoad = SceneManager.UnloadSceneAsync(sceneAct);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
