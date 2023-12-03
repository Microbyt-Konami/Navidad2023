using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace microbytkonamic.navidad
{
    public class PostalesController : MonoBehaviour
    {
        public int anyo;
        public FelicitacionController felicitacionController;
        public AltaFelicitacionController altaFelicitacionController;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(FindAnyObjectByType<ControlerMusic>());
            felicitacionController=FindAnyObjectByType<FelicitacionController>();
            altaFelicitacionController = FindAnyObjectByType<AltaFelicitacionController>();
            felicitacionController.enabled = false;
            altaFelicitacionController.FirstFocus();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
