using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace microbytkonamic.navidad
{
    public class ControlerMusic : MonoBehaviour
    {
        public AudioClip[] musics;

        // Start is called before the first frame update
        void Start()
        {
            if (musics == null || musics.Length == 0)
                return;

            int idx = Random.Range(0, musics.Length);
            var audioSource = GetComponent<AudioSource>();

            audioSource.clip = musics[idx];
            audioSource.Play();
        }
    }
}
