using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using microbytkonamic.proxy;

namespace microbytkonamic.navidad
{
    public class PostalesController : MonoBehaviour
    {
        public int anyo;
        public AudioClip soundButton;
        public AudioClip soundError;
        private AudioSource audioSource;
        private FelicitacionController felicitacionController;
        private static PostalesController instance;

        public static bool isRunning { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                audioSource = GetComponent<AudioSource>();
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                instance.felicitacionController.gameObject.SetActive(true);
                Destroy(this.gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            try
            {
                var controlerMusic = FindAnyObjectByType<ControlerMusic>();
                var proxy = FindAnyObjectByType<MicrobytKonamicProxy>();
                felicitacionController = FindAnyObjectByType<FelicitacionController>();

                var altaFelicitacionController = FindAnyObjectByType<AltaFelicitacionController>();

                altaFelicitacionController.ShowButtons();
                felicitacionController.gameObject.SetActive(false);
                DontDestroyOnLoad(FindAnyObjectByType<ControlerMusic>().gameObject);
                DontDestroyOnLoad(FindAnyObjectByType<MicrobytKonamicProxy>().gameObject);
                DontDestroyOnLoad(felicitacionController.gameObject);
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
            finally
            {
                isRunning = true;
            }
        }

        // Update is called once per frame
        void Update()
        {

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
            if (felicitacionDto != null && intervals != null)
                felicitacionController.StartSetFelicitacion(felicitacionDto, intervals);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
