using System;

public class NaturezaBlocoFerramenta 
{
    public enum NivelDureza
    {
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

    public static Boolean ferramentaQuebraBloco(FerramentaSO ferramenta, BlocosSO bloco)
    {
        return bloco.TipoDureza <= ferramenta.QuebraQueDureza;
    }

    public static Boolean interacaoPodeResultarNaDestruicaoDoBloco(ResultadoInteracao resultado) 
    {
        return resultado == ResultadoInteracao.DESTRUIDO || resultado == ResultadoInteracao.COLETADO;
    }
}
