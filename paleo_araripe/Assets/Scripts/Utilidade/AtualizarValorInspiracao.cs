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
        [SerializeField]private TMP_Text valorInspiracao;
        [Range(0f, 1f)] public float valorInicial = 0f;

        public void Awake()
        {
            sliderUi = GetComponent<Slider>();
            sliderUi.value = valorInicial;
        }

        public void atualizarValor(float valor)
        {
            sliderUi.value = valor;
            valorInspiracao.SetText(valor.ToString());
        }
        public void atualizarValor(float valorA, float valorB)
        {
            valorInspiracao.SetText(valorA.ToString());
            sliderUi.value = valorA / valorB;
        }
        public void atualizarValor(int valorA, int valorB)
        {
            valorInspiracao.SetText(valorA.ToString());
            sliderUi.value = (float)valorA / valorB;
        }


        /// Getters / Setters
        public Slider SliderUi => sliderUi;
    }
}
