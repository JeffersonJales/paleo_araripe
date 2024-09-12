using System;

public class FimPartidaPorTempo : FimPartidaAbstrato
{
    private Boolean tempoPausado = false;
    public override void acabou()
    {
        tempoPausado = true;
        quantidadeParaAcabar = 0;
        controlador.UiSliderTempo.atualizarValorSlider(0f);

        controlador.acabouTempo();
    }

    public void FixedUpdate()
    {
        if(!tempoPausado && --quantidadeParaAcabar <= 0)
            acabou();
    }

    void Update()
    {
        atualizarSliderTempo();
    }
}
