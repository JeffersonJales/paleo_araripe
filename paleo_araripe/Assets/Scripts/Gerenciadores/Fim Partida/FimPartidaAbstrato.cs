using UnityEngine;

public abstract class FimPartidaAbstrato : MonoBehaviour
{
    [SerializeField] protected int quantidadeParaAcabar = 0;
    
    protected int quantidadeTempoInicial = 0;
    protected FimPartidaControlador controlador;

    public void iniciar(int quantidadeFim, FimPartidaControlador controlador) {
        quantidadeParaAcabar = quantidadeFim;
        quantidadeTempoInicial = quantidadeFim;
        this.controlador = controlador;
    }

    public void atualizarSliderTempo()
    {
        controlador.UiSliderTempo.atualizarValorSlider(quantidadeParaAcabar, quantidadeTempoInicial);
    }

    public abstract void acabou();
}
