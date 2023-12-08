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
        public GameObject rootPanel;
        public GameObject buttonsPanel;
        public GameObject dialogPanel;
        public GameObject processPanel;
        public Button anyadirButton;
        public Button showChristmasButton;
        public TMP_Text msgLabel;
        public TMP_InputField nickInputField;
        public TMP_InputField textoInputField;
        public TMP_Text processLabel;

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
            showChristmasButton.onClick.AddListener(ShowChristmas);
            nickInputField.onSubmit.AddListener(nickInputField_OnClick);
            textoInputField.onSubmit.AddListener(textoInputField_OnClick);
            ReCalcMsg();
        }

        void OnDisable()
        {
            anyadirButton.onClick.RemoveListener(ShowDialog);
            showChristmasButton.onClick.RemoveListener(ShowChristmas);
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

        public void ShowChristmas()
        {
            anyadirButton.enabled = false;
            showChristmasButton.enabled = false;
            StartCoroutine(postalesController.LoadScenePostalCoroutine());
        }

        public void HideAll()
        {
            rootPanel.SetActive(false);
            buttonsPanel.SetActive(false);
            dialogPanel.SetActive(false);
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
            postalesController.PlaySoundButton();
            ReCalcMsg();
            if (!string.IsNullOrWhiteSpace(nick))
                textoInputField.Select();
        }

        private void textoInputField_OnClick(string texto)
        {
            postalesController.PlaySoundButton();
            ReCalcMsg();
            if (!string.IsNullOrWhiteSpace(texto))
                DlgSubmit();
        }

        private IEnumerator DlgSubmit_Coroutine(FelicitacionDto felicitacionDto)
        {
            dialogPanel.SetActive(false);
            processPanel.SetActive(true);
            processLabel.text = "Enviando datos al servidor ...";

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
                processLabel.text = ex.Message;
                postalesController.PlaySoundError();

                yield return new WaitForSeconds(5);

                dialogPanel.SetActive(true);
                processPanel.SetActive(false);

                yield break;
            }

            yield return StartCoroutine(postalesController.LoadScenePostalCoroutine(felicitacionDto, intervals));
        }
    }
}
