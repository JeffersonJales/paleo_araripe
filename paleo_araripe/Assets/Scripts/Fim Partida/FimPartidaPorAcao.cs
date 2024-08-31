using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FimPartidaPorAcao : FimPartidaAbstrato
{
    public override void acabou()
    {
        
        sliderTempo.value = 0;
        controlador.acabouTempo();
        controlador.ControladorFerramentas.EventoUsarFerramenta -= utilizouFerramenta;
    }

    public override void iniciarEspecifico()
    {
        controlador.ControladorFerramentas.EventoUsarFerramenta += utilizouFerramenta;
    }

    public void utilizouFerramenta(FerramentaSO ferramenta)
    {
        quantidadeParaAcabar -= ferramenta.TempoGastoAposUso;
        atualizarSliderTempo();

        if (quantidadeParaAcabar <= 0)
        {
            acabou();
        }
    }
}
