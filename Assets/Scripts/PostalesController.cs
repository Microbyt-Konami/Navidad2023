using microbytkonamic.proxy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            felicitacionController = FindAnyObjectByType<FelicitacionController>();
            altaFelicitacionController = FindAnyObjectByType<AltaFelicitacionController>();
            felicitacionController.enabled = false;
            altaFelicitacionController.ShowButtons();
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(FindAnyObjectByType<ControlerMusic>());
            DontDestroyOnLoad(felicitacionController);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator LoadScenePostalCoroutine(FelicitacionDto felicitacionDto = null, IntegerIntervals intervals = null)
        {
            if (felicitacionDto != null && intervals != null)
                felicitacionController.SetFelicitacion(felicitacionDto, intervals);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            felicitacionController.enabled = true;
        }
    }
}
