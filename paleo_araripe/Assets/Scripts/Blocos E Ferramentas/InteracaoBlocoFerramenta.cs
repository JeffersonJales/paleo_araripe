using System;
using System.Collections.Generic;
using static NaturezaBlocoFerramenta;

public class InteracaoBlocoFerramenta {

    private ResumoInteracaoBlocoFerramenta resumoGeral = new ResumoInteracaoBlocoFerramenta();

    public ResumoInteracaoBlocoFerramenta interacaoFerramentaComBloco(FerramentaSO ferramenta, List<BlocoGenerico> blocosGenericos)
    {
        resumoGeral.FerramentaUsada = ferramenta;

        foreach (BlocoGenerico bloco in blocosGenericos)
        {
            BlocosSO blocoSO = bloco.BlocoSO;
            resumoGeral.BlocosAfetados.Add(blocoSO);

            switch (blocoSO.Tipo)
            {
                case NaturezaBlocoFerramenta.TipoBloco.NORMAL:
                    resumoGeral.TipoInteracaoBloco.Add(interacaoFerramentaComBlocoNormal(ferramenta, bloco)); 
                    break;


                case NaturezaBlocoFerramenta.TipoBloco.FOSSIL:
                    resumoGeral.TipoInteracaoBloco.Add(interacaoFerramentaComBlocoFossil(ferramenta, bloco));
                    break;

                case NaturezaBlocoFerramenta.TipoBloco.AMBAR:
                    resumoGeral.TipoInteracaoBloco.Add(interacaoFerramentaComBlocoAmbar(ferramenta, bloco));
                    break;

                default:
                    break;

            }
        }

        return resumoGeral;
    }

    private NaturezaBlocoFerramenta.ResultadoInteracao interacaoFerramentaComBlocoNormal(FerramentaSO ferramenta, BlocoGenerico bloco)
    {
        if(NaturezaBlocoFerramenta.ferramentaQuebraBloco(ferramenta, bloco.BlocoSO))
        {
            return blocoTomaDano(ferramenta, bloco);
        }

        return NaturezaBlocoFerramenta.ResultadoInteracao.NULO;
    }

    private NaturezaBlocoFerramenta.ResultadoInteracao interacaoFerramentaComBlocoFossil(FerramentaSO ferramenta, BlocoGenerico bloco)
    {
        switch (ferramenta.Interacao)
        {
            case NaturezaBlocoFerramenta.TipoInteracao.DELICADO:
                resumoGeral.QuantidadeFossilColetado++;
                return blocoColetado(ferramenta, bloco);

            default:
                ResultadoInteracao resultado = blocoTomaDano(ferramenta, bloco);
                if (resultado == ResultadoInteracao.DESTRUIDO)
                    resumoGeral.QuantidadeFossilDestruido++;

                return resultado;
        }
    }

    private NaturezaBlocoFerramenta.ResultadoInteracao interacaoFerramentaComBlocoAmbar(FerramentaSO ferramenta, BlocoGenerico bloco)
    {
        switch (ferramenta.Interacao)
        {
            case NaturezaBlocoFerramenta.TipoInteracao.DELICADO:
                resumoGeral.QuantidadeAmbarColetado++;
                return blocoColetado(ferramenta, bloco);

            default:
                return blocoTomaDano(ferramenta, bloco);
        }

    }

    private NaturezaBlocoFerramenta.ResultadoInteracao blocoTomaDano(FerramentaSO ferramenta, BlocoGenerico bloco)
    {
        if (bloco.tomarDano(ferramenta.Dano))
            return NaturezaBlocoFerramenta.ResultadoInteracao.DESTRUIDO;
        else
            return NaturezaBlocoFerramenta.ResultadoInteracao.DANO;
    }

    private NaturezaBlocoFerramenta.ResultadoInteracao blocoColetado(FerramentaSO ferramenta, BlocoGenerico bloco)
    {
        bloco.serColetado();
        return NaturezaBlocoFerramenta.ResultadoInteracao.COLETADO;
    }

}
