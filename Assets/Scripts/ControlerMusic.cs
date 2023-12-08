using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace microbytkonamic.navidad
{
    public class ControlerMusic : MonoBehaviour
    {
        public AudioClip[] musics;

        private AudioSource audioSource;

        private void Awake()
        {
            if (PostalesController.isRunning)
                Destroy(this.gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();

            if (musics == null || musics.Length == 0)
                return;

            int idx = Random.Range(0, musics.Length);

            audioSource.clip = musics[idx];
            audioSource.Play();
        }
    }
}
