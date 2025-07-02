using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PaleoAraripe {
    /// <summary>
    /// Classe responsável por atualizar o valor de um Slider na interface, permitindo diferentes formas de atualização.
    /// </summary>
    public class AtualizarValorSlider : MonoBehaviour
    {
        private Slider sliderUi;
        [Range(0f, 1f)] public float valorInicial = 0f;

        public void Awake()
        {
            sliderUi = GetComponent<Slider>();    
            sliderUi.value = valorInicial; 
        }

        public void atualizarValorSlider(float valor) {
            sliderUi.value = valor;
        }
        public void atualizarValorSlider(float valorA, float valorB) {
            sliderUi.value = valorA / valorB;
        }
        public void atualizarValorSlider(int valorA, int valorB) {
            sliderUi.value = (float)valorA / valorB;
        }


        /// Getters / Setters
        public Slider SliderUi => sliderUi;
    }
}
