using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace microbytkonamic.navidad
{
    public class Lights : MonoBehaviour
    {
        public GameObject emissiveObject;
        public Material emissiveMaterialOn;
        public Material emissiveMaterialOff;
        public float timeChangeOnOff = .5f;

        Renderer _renderer;
        float time = 0;
        bool on = true;

        // Start is called before the first frame update
        void Start()
        {
            _renderer = emissiveObject.GetComponent<Renderer>();
            _renderer.material = emissiveMaterialOn;
        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;

            if (time > timeChangeOnOff)
            {
                time = 0;
                on = !on;
                _renderer.material = on ? emissiveMaterialOn : emissiveMaterialOff;
            }
        }
    }
}
