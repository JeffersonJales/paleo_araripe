using UnityEngine;

public class ControladorSliderInspiracao : MonoBehaviour
{
    [SerializeField] private AtualizarValorSlider valorSlider;
    [SerializeField] private BlackBoardInformacoesPartida informacoesPartida;
    
    void Start()
    {
        UsarFerramentas usarFerramenta = FindObjectOfType<UsarFerramentas>();

        if (usarFerramenta != null) { 
            usarFerramenta.EventoAposRealizarUsoFerramenta += autualizarSliderInspiracao;
            atualizarValorSlider(usarFerramenta.InspiracaoAtual, usarFerramenta.InspiracaoMaxima);
        }
    }

    private void autualizarSliderInspiracao(ResumoInteracaoBlocoFerramenta resumo) {
        if (resumo.ganhouInspiraca() || resumo.gastouInspiaracao())
            atualizarValorSlider(informacoesPartida.GetIntValue(informacoesPartida.INSPIRACAO_ATUAL), informacoesPartida.GetIntValue(informacoesPartida.INSPIRACAO_MAXIMA));
    }

    private void atualizarValorSlider(int inspiracaoAtual, int inspiracaoMaxima)
    {
        valorSlider.atualizarValorSlider(inspiracaoAtual, inspiracaoMaxima);
    }
}
