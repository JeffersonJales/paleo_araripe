using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FimPartidaPorTempo : FimPartidaAbstrato
{
    private Boolean tempoPausado = false;
    public override void acabou()
    {
        tempoPausado = true;
        sliderTempo.value = 0;
        quantidadeParaAcabar = 0;
        controlador.acabouTempo();
    }

    public void FixedUpdate()
    {
        if(!tempoPausado && --quantidadeParaAcabar <= 0)
            acabou();
    }

    public override void iniciarEspecifico()
    {
    }

    void Update()
    {
        atualizarSliderTempo();
    }
}
