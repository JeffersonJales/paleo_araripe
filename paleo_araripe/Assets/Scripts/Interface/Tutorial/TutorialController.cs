using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PaleoAraripe
{
    public class TutorialController : MonoBehaviour
    {
        [Header("Slides do Tutorial")]
        public GameObject[] slides;

        [Header("Botões")]
        public Button botaoAvancar;
        public Button botaoVoltar;

        private int slideAtual = 0;

        void Start()
        {
            AtualizarSlides();
            AtualizarBotoes();
        }

        public void Avancar()
        {
            if (slideAtual < slides.Length - 1)
            {
                slideAtual++;
                AtualizarSlides();
                AtualizarBotoes();
            }
        }

        public void Voltar()
        {
            if (slideAtual > 0)
            {
                slideAtual--;
                AtualizarSlides();
                AtualizarBotoes();
            }
        }

        private void AtualizarSlides()
        {
            for (int i = 0; i < slides.Length; i++)
            {
                slides[i].SetActive(i == slideAtual);
            }
        }

        private void AtualizarBotoes()
        {
            botaoVoltar.interactable = slideAtual > 0;
            botaoAvancar.interactable = slideAtual < slides.Length - 1;
        }
    }
}
