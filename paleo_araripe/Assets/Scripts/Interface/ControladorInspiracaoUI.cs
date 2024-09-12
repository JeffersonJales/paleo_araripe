using UnityEngine;

public class ControladorInspiracaoUI : MonoBehaviour
{
    [SerializeField] private AtualizarValorSlider valorSlider;
    [SerializeField] private QuadroNegroSO informacoesJogo;
    
    void Start()
    {
        UsarFerramentas usarFerramenta = FindObjectOfType<UsarFerramentas>();

        if (usarFerramenta != null) { 
            usarFerramenta.EventoResumoInteracao += autualizarSliderInspiracao;
            atualizarValorSlider(usarFerramenta.InspiracaoAtual, usarFerramenta.InspiracaoMaxima);
        }
    }

    private void autualizarSliderInspiracao(ResumoInteracaoBlocoFerramenta resumo) {
        if (resumo.ganhouInspiraca() || resumo.gastouInspiaracao())
            atualizarValorSlider(informacoesJogo.GetIntValue(QuadroNegroInfoJogoChaves.INSPIRACAO_ATUAL), informacoesJogo.GetIntValue(QuadroNegroInfoJogoChaves.INSPIRACAO_MAXIMA));
    }

    private void atualizarValorSlider(int inspiracaoAtual, int inspiracaoMaxima)
    {
        valorSlider.atualizarValorSlider(inspiracaoAtual, inspiracaoMaxima);
    }
}
