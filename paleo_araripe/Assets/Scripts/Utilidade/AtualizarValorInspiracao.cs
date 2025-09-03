using System;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PaleoAraripe
{
    /// <summary>
    /// Classe responsável por atualizar o valor de um Slider na interface, permitindo diferentes formas de atualização.
    /// </summary>
    public class AtualizarValorInspiracao : MonoBehaviour
    {
        private Slider sliderUi;
        [SerializeField] private TMP_Text valorInspiracao;

        
        [SerializeField] private Slider sliderFeedback;
        [SerializeField] private Animator animatorFeedback;

        private float valorAtual = 0f;
        private float valorMaximo = 1f;
        private FerramentaSO ferramentaAtual;

        public void Awake()
        {
            sliderUi = GetComponent<Slider>();
            sliderUi.value = 0;
        }

        public void ferramentaAtualizada(FerramentaSO ferramenta)
        {
            ferramentaAtual = ferramenta;

            bool ativarFeedback = ferramenta != null && ferramenta.Inspiracao != 0;
            sliderFeedback.gameObject.SetActive(ativarFeedback);

            if (ativarFeedback)
            {
                if (ferramenta.Inspiracao < 0)
                {
                    animatorFeedback.SetTrigger("perder");
                    sliderUi.value = (float) (valorAtual + ferramenta.Inspiracao) / valorMaximo;
                    sliderFeedback.value = (float) valorAtual / valorMaximo;
                }
                else
                {
                    animatorFeedback.SetTrigger("ganhar");
                    sliderUi.value = (float) valorAtual / valorMaximo; 
                    sliderFeedback.value = (float) (valorAtual + ferramenta.Inspiracao) / valorMaximo;
                }
            }
            else
            {
                sliderUi.value = (float) valorAtual / valorMaximo;
            }
        }

        public void atualizarValor(int valorAtual, int valorMaximo)
        {
            valorInspiracao.SetText(valorAtual.ToString());
            this.valorAtual = valorAtual;
            this.valorMaximo = valorMaximo;
            ferramentaAtualizada(ferramentaAtual);
        }

        /// Getters / Setters
        public Slider SliderUi => sliderUi;
    }
}
