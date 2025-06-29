using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaleoAraripe {
    public class ControleFimPartida : MonoBehaviour
    {
        [Range(10, 1000)]
        [SerializeField] private int acoesParaFimJogo = 100;

        [Range(1, 100)]
        [SerializeField] private int acoesGanhasPorAmbar = 0;
        [SerializeField] private int quantidadeFossils = 0;

        [SerializeField] private AtualizarValorSlider uiSliderTempo; // Slider para mostrar quantidade de tempo

        [SerializeField] private Animator animacaoCarregamento;

        [SerializeField] private GameObject telaFinal;

        private ControladorFerramenta controladorFerramentas;
        private int quantidadeAcoesInicial = 0;

        /// Getters / Setters
        public AtualizarValorSlider UiSliderTempo => uiSliderTempo;


        void Start()
        {
            controladorFerramentas = GetComponent<ControladorFerramenta>();

            quantidadeAcoesInicial = acoesParaFimJogo;
            controladorFerramentas.EventoAposRealizarUsoFerramenta += verificarFimFosseis;
            controladorFerramentas.EventoAposRealizarUsoFerramenta += verificarFimSemAcoes;

            /// Catar quantidade de fssies para acabar o jogo
            BlocoGenerico[] blocosGenericosNaCena = FindObjectsOfType<BlocoGenerico>();
            foreach(var item in blocosGenericosNaCena)
            {
                if(item.BlocoSO.Tipo == NaturezaBlocoFerramenta.TipoBloco.FOSSIL)
                    quantidadeFossils++;
            }

            uiSliderTempo.atualizarValorSlider(1f);
        }

        public void verificarFimFosseis(ResumoInteracaoBlocoFerramenta resumo) {
            quantidadeFossils -= resumo.QuantidadeFossilColetado;
            quantidadeFossils -= resumo.QuantidadeFossilDestruido;
            if (quantidadeFossils <= 0)
                finalizarPartida();
        }

        public void verificarFimSemAcoes(ResumoInteracaoBlocoFerramenta resumo)
        {
            acoesParaFimJogo -= resumo.FerramentaUsada.TempoGastoAposUso;
            acoesParaFimJogo = Math.Min(acoesParaFimJogo + acoesGanhasPorAmbar * resumo.QuantidadeAmbarColetado, quantidadeAcoesInicial);
            uiSliderTempo.atualizarValorSlider(acoesParaFimJogo, quantidadeAcoesInicial);

            if (acoesParaFimJogo <= 0)
                finalizarPartida();
        }

        public void finalizarPartida()
        {
            GerenciadorDados.Instance.jogoFinalizado = true;
            telaFinal.SetActive(true);
        }
        public void VoltarParaMenu()
        {
            animacaoCarregamento.SetTrigger("carregar");
        }
    }
}
