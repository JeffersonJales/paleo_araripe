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
        DELICADO = 0,
        MODERADO,
        PESADO
    }

    public static Boolean ferramentaQuebraBloco(BlocoGenerico bloco, FerramentaSO ferramenta)
    {
        return bloco.BlocoSO.TipoDureza <= ferramenta.QuebraQueDureza;
    }
}
