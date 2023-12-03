using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace microbytkonamic.navidad
{
    public class AltaFelicitacionController : MonoBehaviour
    {
        public TMP_InputField nickInputField;
        public TMP_InputField textoInputField;

        // Start is called before the first frame update
        void Start()
        {
            nickInputField.onSubmit.AddListener(t => textoInputField.Select());
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void FirstFocus()
        {
            nickInputField.Select();
        }
    }
}
