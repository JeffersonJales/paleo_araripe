using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class FimPartidaAbstrato : MonoBehaviour
{
    [SerializeField] protected int quantidadeParaAcabar = 0;
    
    protected int quantidadeTempoInicial = 0;
    protected FimPartidaControlador controlador;
    protected Slider sliderTempo;

    public void iniciar(int quantidadeFim, FimPartidaControlador controlador) {
        quantidadeParaAcabar = quantidadeFim;
        quantidadeTempoInicial = quantidadeFim;

        sliderTempo = controlador.UiSliderTempo;
        this.controlador = controlador;
    }

    public void atualizarSliderTempo()
    {
        sliderTempo.value = (float)quantidadeParaAcabar / quantidadeTempoInicial;
    }

    public abstract void acabou();
}
