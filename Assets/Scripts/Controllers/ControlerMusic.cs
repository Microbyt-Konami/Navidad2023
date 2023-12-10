using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

namespace microbytkonamic.navidad
{
    public class ControlerMusic : MonoBehaviourSingleton<ControlerMusic>
    {
        public AudioClip[] musics;

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
            if (musics == null || musics.Length == 0)
                return;

            int idx = Random.Range(0, musics.Length);

            audioSource.clip = musics[idx];
            audioSource.Play();
        }
    }
}
