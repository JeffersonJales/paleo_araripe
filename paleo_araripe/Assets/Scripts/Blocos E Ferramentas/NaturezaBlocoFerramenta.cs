using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturezaBlocoFerramenta 
{
    public enum NivelDureza
    {
        TERRA = 0,
        AREIA,
        PEDRA,
        FERRO,
    }   

    public enum TipoBloco
    {
        NORMAL = 0,
        AMBAR, 
        FOSSIL
    }

    public enum TipoInteracao
    {
        SONDAR = 0,
        DELICADO,
        MODERADO,
        PESADO
    }

    public enum ResultadoInteracao
    {
        NULO = 0,
        DANO,
        DESTRUIDO,
        COLETADO
    }

    public static Boolean ferramentaQuebraBloco(FerramentaSO ferramenta, BlocosSO bloco)
    {
        return bloco.TipoDureza <= ferramenta.QuebraQueDureza;
    }

    public static Boolean interacaoPodeResultarNaDestruicaoDoBloco(ResultadoInteracao resultado) 
    {
        return resultado == ResultadoInteracao.DESTRUIDO || resultado == ResultadoInteracao.COLETADO;
    }
}
