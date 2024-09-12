using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class NaturezaBlocoFerramenta 
{
    public enum NivelDureza
    {
        NULO,
        TERRA,
        AREIA,
        PEDRA,
        FERRO,
    }   

    public enum TipoBloco
    {
        NORMAL,
        AMBAR, 
        FOSSIL
    }

    public enum TipoInteracao
    {
        SONDAR,
        DELICADO,
        MODERADO,
        PESADO
    }

    public enum ResultadoInteracao
    {
        NULO,
        DANO,
        DESTRUIDO,
        COLETADO
    }

    public enum TipoColisaoFerramenta
    {
        NULO,
        PONTO,
        ESCOVAR,
        PERFURAR,
        VASCULHAR,
        CAMERA
    }

    public static Boolean ferramentaQuebraBloco(FerramentaSO ferramenta, BlocoSO bloco)
    {
        return bloco.TipoDureza <= ferramenta.QuebraQueDureza;
    }

    public static Boolean interacaoPodeResultarNaDestruicaoDoBloco(ResultadoInteracao resultado) 
    {
        return resultado == ResultadoInteracao.DESTRUIDO || resultado == ResultadoInteracao.COLETADO;
    }

    public static List<GameObject> obterListaBlocosPorFerramenta(FerramentaSO ferramenta, GameObject blocoFoco, Vector3 normal)
    {
        switch (ferramenta.TipoColisao)
        {
            case TipoColisaoFerramenta.PONTO: return new ColisaoFerramentaPonto().obterBlocos(blocoFoco, normal);
            case TipoColisaoFerramenta.ESCOVAR: return new ColisaoFerramentaEscovar().obterBlocos(blocoFoco, normal);
            case TipoColisaoFerramenta.PERFURAR: return new ColisaoFerramentaPerfurar().obterBlocos(blocoFoco, normal);
            case TipoColisaoFerramenta.VASCULHAR: return new ColisaoFerramentaVasculhar().obterBlocos(blocoFoco, normal);
            case TipoColisaoFerramenta.CAMERA: return new ColisaoFerramentaCamera().obterBlocos(blocoFoco, normal);
                   
            case TipoColisaoFerramenta.NULO:
            default:
                return new List<GameObject>();
        }
    }
}
