using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;
using TMPro;

using microbytkonamic.proxy;
using static UnityEngine.Networking.UnityWebRequest;

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

        public void SetFelicitacion(FelicitacionDto felicitacionDto, IntegerIntervals intervals)
        {
            nickText.text = felicitacionDto.nick;
            //Enviada 30 de Diciembre de 2023
            fechaText.text = $"Enviada el {(System.DateTime)felicitacionDto?.fecha:D}";
            textoText.text = felicitacionDto.texto;
            input.Intervals = intervals;
            estado = EstadosFelicitacion.Felicitacion;
            print(intervals);
        }

        public void StartSetFelicitacion(FelicitacionDto felicitacionDto, IntegerIntervals intervals)
        {
            StartCoroutine(GetFelicitacion_Coroutine(felicitacionDto, intervals));
        }

        private void Awake()
        {
            if (PostalesController.isRunning)
            {
                Destroy(this.gameObject);

                return;
            }
            estado = EstadosFelicitacion.GetFelicitacion;
            input = new GetFelicitacionIn
            {
                Intervals = new IntegerIntervals
                {
                    intervals = new IntegerInterval[0]
                }
            };
            nickText.text = fechaText.text = textoText.text = string.Empty;
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

            yield return StartCoroutine(proxy.GetFelicitacion(input, GetFelicitacion_Callback));
        }

        IEnumerator GetFelicitacion_Callback(System.Exception ex, GetFelicitacionResult result)
        {
            if (terminado)
                yield break;

            if (result == null)
            {
                estado = EstadosFelicitacion.GetFelicitacion;
                yield break;
            }

            //nickText.text = result.felicitacionDto.nick;
            ////Enviada 30 de Diciembre de 2023
            //fechaText.text = $"Enviada el {(System.DateTime)result.felicitacionDto?.fecha:D}";
            //textoText.text = result.felicitacionDto.texto;
            //input.Intervals = result.intervals;
            yield return StartCoroutine(GetFelicitacion_Coroutine(result.felicitacionDto, result.intervals));
        }

        IEnumerator GetFelicitacion_Coroutine(FelicitacionDto felicitacionDto, IntegerIntervals intervals)
        {
            SetFelicitacion(felicitacionDto, intervals);

            yield return new WaitForSeconds(interval);

            if (terminado)
                yield break;

            estado = EstadosFelicitacion.GetFelicitacion;
        }
    }
}
