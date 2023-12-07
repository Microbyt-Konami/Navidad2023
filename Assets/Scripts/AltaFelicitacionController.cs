using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

using microbytkonamic.proxy;
using System.Text;

namespace microbytkonamic.navidad
{
    public class AltaFelicitacionController : MonoBehaviour
    {
        public GameObject buttonsPanel;
        public GameObject dialogPanel;
        public Button anyadirButton;
        public TMP_Text msgLabel;
        public TMP_InputField nickInputField;
        public TMP_InputField textoInputField;

        MicrobytKonamicProxy proxy;
        PostalesController postalesController;

        private void Awake()
        {
            nickInputField.text = textoInputField.text = string.Empty;
            buttonsPanel.SetActive(false);
            dialogPanel.SetActive(false);
        }

        void OnEnable()
        {
            anyadirButton.onClick.AddListener(ShowDialog);
            nickInputField.onSubmit.AddListener(nickInputField_OnClick);
            textoInputField.onSubmit.AddListener(textoInputField_OnClick);
            ReCalcMsg();
        }

        void OnDisable()
        {
            anyadirButton.onClick.RemoveListener(ShowDialog);
            nickInputField.onSubmit.RemoveListener(nickInputField_OnClick);
            textoInputField.onSubmit.RemoveListener(textoInputField_OnClick);
        }

        // Start is called before the first frame update
        void Start()
        {
            proxy = FindObjectOfType<MicrobytKonamicProxy>();
            postalesController = FindAnyObjectByType<PostalesController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void FirstFocus()
        {
            nickInputField.Select();
        }

        public void ShowButtons()
        {
            buttonsPanel.SetActive(true);
            dialogPanel.SetActive(false);
        }

        public void ShowDialog()
        {
            buttonsPanel.SetActive(false);
            dialogPanel.SetActive(true);
            nickInputField.Select();
        }

        public void ReCalcMsg()
        {
            var sb = new StringBuilder();
            var verbo = "es";

            if (string.IsNullOrWhiteSpace(nickInputField.text))
                sb.Append("Nick");
            if (string.IsNullOrWhiteSpace(textoInputField.text))
            {
                if (sb.Length > 0)
                {
                    sb.Append(" y ");
                    verbo = "son";
                }
                sb.Append("Felicitación");
            }

            if (sb.Length > 0)
                sb.Append($"{verbo} requeridos");

            msgLabel.text = sb.ToString();
        }

        public void DlgSubmit()
        {
            var felicitacionDto = new FelicitacionDto
            {
                fecha = DateTime.Now,
                nick = nickInputField.text,
                texto = textoInputField.text,
            };

            StartCoroutine(DlgSubmit_Coroutine(felicitacionDto));
        }

        private void nickInputField_OnClick(string nick)
        {
            ReCalcMsg();
            if (!string.IsNullOrWhiteSpace(nick))
                textoInputField.Select();
        }

        private void textoInputField_OnClick(string texto)
        {
            ReCalcMsg();
            if (!string.IsNullOrWhiteSpace(texto))
                DlgSubmit();
        }

        private IEnumerator DlgSubmit_Coroutine(FelicitacionDto felicitacionDto)
        {
            var input = new AltaFelicitacionIn
            {
                anyo = postalesController.anyo,
                felicitacionDto = felicitacionDto
            };

            yield return StartCoroutine(proxy.AltaFelicitacion(input, (ex, i) => AltaFelicitacion_Callback(felicitacionDto, ex, i)));
        }

        private IEnumerator AltaFelicitacion_Callback(FelicitacionDto felicitacionDto, Exception ex, IntegerIntervals intervals)
        {
            if (ex != null)
            {
                msgLabel.text = ex.Message;

                yield break;
            }
            yield break;
        }
    }
}
