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
        [SerializeField] private Slider sliderFeedback;
        [SerializeField] private Animator animatorFeedback;

        private float valorAtual = 1f;
        private float valorMaximo = 1f;
        private FerramentaSO ferramentaAtual;

        public void Awake()
        {
            sliderUi = GetComponent<Slider>();    
            sliderUi.value = 1f; 
        }


        public void atualizarValorSlider(int valorAtual, int valorMaximo) {
            this.valorAtual = valorAtual;
            this.valorMaximo = valorMaximo;
            atualizarSlider(ferramentaAtual);
        }

        public void atualizarSlider(FerramentaSO ferramenta) {
            ferramentaAtual = ferramenta;
            bool ativarFeedback = ferramenta != null && ferramenta.TempoGastoAposUso > 0;
            
            sliderFeedback.gameObject.SetActive(ativarFeedback);
            if (ativarFeedback)
            {
                sliderUi.value = (float) (valorAtual - ferramenta.TempoGastoAposUso) / valorMaximo;
                sliderFeedback.value = (float) valorAtual / valorMaximo;
            }
            else
            {
                sliderUi.value = (float) valorAtual / valorMaximo;
            }
        }


        /// Getters / Setters
        public Slider SliderUi => sliderUi;
    }
}
