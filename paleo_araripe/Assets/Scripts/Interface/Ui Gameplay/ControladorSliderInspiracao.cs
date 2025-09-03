using System;
using UnityEngine;

namespace PaleoAraripe {
    /// <summary>
    /// Classe responsável por atualizar o slider de inspiração na interface de acordo com as ações do jogador.
    /// </summary>
    public class ControladorSliderInspiracao : MonoBehaviour
    {
        [SerializeField] private AtualizarValorInspiracao valorSlider;
        [SerializeField] private BlackBoardInformacoesPartida informacoesPartida;
        
        void Start()
        {
            ControladorFerramenta usarFerramenta = FindObjectOfType<ControladorFerramenta>();

            if (usarFerramenta != null) { 
                usarFerramenta.EventoAposRealizarUsoFerramenta += autualizarSliderInspiracao;
                atualizarValorSlider(usarFerramenta.InspiracaoAtual, usarFerramenta.InspiracaoMaxima);
            }

            foreach (TrocarFerramentaViaBotaoUI item in FindObjectsOfType<TrocarFerramentaViaBotaoUI>())
            {
                item.eventoTentativaTrocaFerramenta += TrocouFerramenta;
            }
        }

        private void autualizarSliderInspiracao(ResumoInteracaoBlocoFerramenta resumo) {
            if (resumo.ganhouInspiraca() || resumo.gastouInspiaracao())
                atualizarValorSlider(informacoesPartida.GetIntValue(informacoesPartida.INSPIRACAO_ATUAL), informacoesPartida.GetIntValue(informacoesPartida.INSPIRACAO_MAXIMA));
        }

        private void atualizarValorSlider(int inspiracaoAtual, int inspiracaoMaxima)
        {
            valorSlider.atualizarValor(inspiracaoAtual, inspiracaoMaxima);
        }

        private void TrocouFerramenta(FerramentaSO ferramentSo)
        {
            valorSlider.ferramentaAtualizada(ferramentSo);
        }
    }
}
