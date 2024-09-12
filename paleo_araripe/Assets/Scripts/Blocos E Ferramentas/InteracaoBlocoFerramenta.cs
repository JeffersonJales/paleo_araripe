using System.Collections.Generic;
using static NaturezaBlocoFerramenta;

public class InteracaoBlocoFerramenta {

    private ResumoInteracaoBlocoFerramenta resumoGeral = new ResumoInteracaoBlocoFerramenta();

    public ResumoInteracaoBlocoFerramenta interacaoFerramentaComBloco(FerramentaSO ferramenta, List<BlocoGenerico> blocosGenericos)
    {
        resumoGeral.FerramentaUsada = ferramenta;

        foreach (BlocoGenerico bloco in blocosGenericos)
        {
            BlocoSO blocoSO = bloco.BlocoSO;
            resumoGeral.BlocosAfetados.Add(blocoSO);

            if (ferramenta.DaDanoEm.Contains(blocoSO))
            {
                resumoGeral.TipoInteracaoBloco.Add(blocoTomaDano(ferramenta, bloco));
            }
            else if (ferramenta.ConsegueColetar.Contains(blocoSO))
            {
                resumoGeral.TipoInteracaoBloco.Add(blocoColetado(ferramenta, bloco));
            }
            else
            {
                resumoGeral.TipoInteracaoBloco.Add(NaturezaBlocoFerramenta.ResultadoInteracao.NULO);
            }
        }

        return resumoGeral;
    }

    private ResultadoInteracao blocoTomaDano(FerramentaSO ferramenta, BlocoGenerico bloco)
    {
        switch (bloco.BlocoSO.Tipo)
        {
            case TipoBloco.AMBAR:
            case TipoBloco.NORMAL: 
                return realizarDanoNoBloco(ferramenta, bloco);
            
            case TipoBloco.FOSSIL:
                var resultado = realizarDanoNoBloco(ferramenta, bloco);

                if (resultado.Equals(ResultadoInteracao.DESTRUIDO))
                    resumoGeral.QuantidadeFossilDestruido++;

                return resultado;


            default:
                return ResultadoInteracao.NULO;
        }
    }

    private ResultadoInteracao realizarDanoNoBloco(FerramentaSO ferramenta, BlocoGenerico bloco)
    {
        if (bloco.tomarDano(ferramenta.Dano))
            return NaturezaBlocoFerramenta.ResultadoInteracao.DESTRUIDO;
        else
            return NaturezaBlocoFerramenta.ResultadoInteracao.DANO;
    }

    private ResultadoInteracao blocoColetado(FerramentaSO ferramenta, BlocoGenerico bloco)
    {
        switch (bloco.BlocoSO.Tipo)
        {
            case TipoBloco.AMBAR:
                resumoGeral.QuantidadeAmbarColetado++;
                break;

            case TipoBloco.FOSSIL:
                resumoGeral.QuantidadeFossilColetado++;
                break;
        }
        
        
        bloco.serColetado();
        return NaturezaBlocoFerramenta.ResultadoInteracao.COLETADO;
    }

}
