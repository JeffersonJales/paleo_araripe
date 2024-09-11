using UnityEngine;

public class ControladorInspiracaoUI : MonoBehaviour
{
    [SerializeField] private AtualizarValorSlider valorSlider;

    void Start()
    {
        UsarFerramentas usarFerramenta = FindObjectOfType<UsarFerramentas>();

        if (usarFerramenta != null) { 
            usarFerramenta.InspiracaoAlterada += autualizarSliderInspiracao;
            valorSlider.atualizarValorSlider(usarFerramenta.InspiracaoAtual, usarFerramenta.InspiracaoMaxima);
        }
    }

    private void autualizarSliderInspiracao(int inspiracaoAtual, int inspiracaoTotal) {
        valorSlider.atualizarValorSlider(inspiracaoAtual, inspiracaoTotal);
    }
}
