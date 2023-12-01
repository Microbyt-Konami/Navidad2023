using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using microbytkonamic.proxy;
using Unity.Collections;
using TMPro;

namespace microbytkonamic.navidad
{
    public class FelicitacionController : MonoBehaviour
    {
        public bool inicio;
        public bool terminado;
        public float interval = 5;  // segundos
        public TextMeshProUGUI nickText;
        public TextMeshProUGUI fechaText;
        public TextMeshProUGUI textoText;

        [SerializeField, ReadOnly]
        EstadosFelicitacion estado;
        MicrobytKonamicProxy proxy;
        PostalesController postalesController;
        GetFelicitacionIn input;

        private void Awake()
        {
            estado = EstadosFelicitacion.GetFelicitacion;
            input = new GetFelicitacionIn
            {
                Intervals = new IntegerIntervals
                {
                    intervals = new IntegerInterval[0]
                }
            };
            nickText.text = "";
            fechaText.text = "";
            textoText.text = "";
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
            if (inicio && !terminado)
                switch (estado)
                {
                    case EstadosFelicitacion.GetFelicitacion:
                        StartCoroutine(GetFelicitacion());
                        break;
                }
        }

        IEnumerator GetFelicitacion()
        {
            estado = EstadosFelicitacion.WaitFelicitacion;
            input.Anyo = postalesController.anyo;

            yield return StartCoroutine(proxy.PostCoroutine(input, Callback_GetFelicitacion));
        }

        IEnumerator Callback_GetFelicitacion(System.Exception ex, GetFelicitacionResult result)
        {
            if (terminado)
                yield break;

            if (result == null)
            {
                estado = EstadosFelicitacion.GetFelicitacion;
                yield break;
            }

            nickText.text = result.felicitacionDto.nick;
            //Enviada 30 de Diciembre de 2023
            fechaText.text = $"Enviada el {result.felicitacionDto?.fecha:D}";
            textoText.text = result.felicitacionDto.texto;
            input.Intervals = result.intervals;

            print(result.intervals);

            estado = EstadosFelicitacion.Felicitacion;

            yield return new WaitForSeconds(interval);

            if (terminado)
                yield break;

            estado = EstadosFelicitacion.GetFelicitacion;
        }
    }
}
