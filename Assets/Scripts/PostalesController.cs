using microbytkonamic.proxy;
using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace microbytkonamic.navidad
{
    public class PostalesController : MonoBehaviour
    {
        public int anyo;
        public AudioClip soundButton;
        public AudioClip soundError;
        private AudioSource audioSource;
        private FelicitacionController felicitacionController;
        private AltaFelicitacionController altaFelicitacionController;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            felicitacionController = FindAnyObjectByType<FelicitacionController>();
            altaFelicitacionController = FindAnyObjectByType<AltaFelicitacionController>();
            felicitacionController.enabled = false;
            altaFelicitacionController.ShowButtons();
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(FindAnyObjectByType<ControlerMusic>());
            DontDestroyOnLoad(felicitacionController);
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
                felicitacionController.SetFelicitacion(felicitacionDto, intervals);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            felicitacionController.enabled = true;
        }
    }
}
