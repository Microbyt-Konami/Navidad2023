using System.Collections;
using System.Collections.Generic;
using System.Transactions;

using UnityEngine;

using microbytkonamic.proxy;

namespace microbytkonamic.navidad
{
    public class ControlerMusic : MonoBehaviourSingleton<ControlerMusic>
    {
        public AudioClip[] musics;
        public bool isMusicsRemote = true;

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
            if (isMusicsRemote)
            {
                StartCoroutine(MicrobytKonamicProxy.Instance.MusicaNavidadMP3(MusicaNavidadMP3_Callback));

                return;
            }
            if (musics == null || musics.Length == 0)
                return;

            int idx = Random.Range(0, musics.Length);

            audioSource.clip = musics[idx];
            audioSource.Play();
        }

        IEnumerator MusicaNavidadMP3_Callback(System.Exception ex, AudioClip audioClip)
        {
            if (audioClip != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }

            yield return null;
        }
    }
}
