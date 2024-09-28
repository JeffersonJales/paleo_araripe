using System.Collections.Generic;
using System.Numerics;
using static NaturezaBlocoFerramenta;

public class InteracaoBlocoFerramenta {

    private ResumoInteracaoBlocoFerramenta resumoGeral = new ResumoInteracaoBlocoFerramenta();

    public ResumoInteracaoBlocoFerramenta interacaoFerramentaComBloco(FerramentaSO ferramenta, List<BlocoGenerico> blocosGenericos)
    {
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
                resumoGeral.TipoInteracaoBloco.Add(ResultadoInteracao.NULO);
            }
        }

        resumoGeral.FerramentaUsada = ferramenta;

        resumoGeral.AlgumBlocoDestruidoOuColeado =
            resumoGeral.QuantidadeFossilDestruido > 0 ||
            resumoGeral.QuantidadeFossilColetado > 0 ||
            resumoGeral.QuantidadeAmbarColetado > 0 ||
            resumoGeral.TipoInteracaoBloco.Contains(ResultadoInteracao.DESTRUIDO);

        return resumoGeral;
    }

    public ResumoInteracaoBlocoFerramenta interacaoFerramentaComBloco(FerramentaSO ferramenta, List<BlocoGenerico> blocosGenericos, ResumoInteracaoBlocoFerramenta resumo)
    {
        resumoGeral = resumo;
        return interacaoFerramentaComBloco(ferramenta, blocosGenericos);
    }

    private ResultadoInteracao blocoTomaDano(FerramentaSO ferramenta, BlocoGenerico bloco)
    {
        ResultadoInteracao resultado;

        switch (bloco.BlocoSO.Tipo)
        {
            case TipoBloco.AMBAR:
            case TipoBloco.NORMAL: 
                return realizarDanoNoBloco(ferramenta, bloco);
            
            case TipoBloco.FOSSIL:
                resultado = realizarDanoNoBloco(ferramenta, bloco);

                if (resultado.Equals(ResultadoInteracao.DESTRUIDO)) {
                    resumoGeral.QuantidadeFossilDestruido++;
                }

                return resultado;


            case TipoBloco.INSPIRACAO:
                resultado = realizarDanoNoBloco(ferramenta, bloco);
                receberInspiracao(resultado, bloco);
                
                return resultado;

            case TipoBloco.EXPLOSIVO:
                return tentarExplodir(realizarDanoNoBloco(ferramenta, bloco), bloco);
            default:
                return ResultadoInteracao.NULO;
        }
    }

    private ResultadoInteracao realizarDanoNoBloco(FerramentaSO ferramenta, BlocoGenerico bloco)
    {
        if (bloco.tomarDano(ferramenta.Dano)) {
            resumoGeral.BlocosDestruidos.Add(bloco.gameObject);

            return ResultadoInteracao.DESTRUIDO;
        }
        else
            return ResultadoInteracao.DANO;
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

            case TipoBloco.INSPIRACAO:
                receberInspiracao(ResultadoInteracao.COLETADO, bloco);
                break;

            default:
                return ResultadoInteracao.NULO;
        }
        
        
        bloco.aoSerColetado();
        return ResultadoInteracao.COLETADO;
    }

    private void receberInspiracao(ResultadoInteracao resultadoInteracao, BlocoGenerico bloco)
    {
        if (resultadoInteracao.Equals(ResultadoInteracao.DESTRUIDO) || resultadoInteracao.Equals(ResultadoInteracao.COLETADO))
        {
            BlocoInspiracao blocoInspiracao = (BlocoInspiracao)bloco;
            resumoGeral.QuantidadeInspiracaoGanha += blocoInspiracao.Inspiracao;
        }
    }

    private ResultadoInteracao tentarExplodir(ResultadoInteracao resultadoDano, BlocoGenerico bloco)
    {
        if (resultadoDano.Equals(ResultadoInteracao.DESTRUIDO)) {
            var blocoExplosivo = (BlocoExplosivo)bloco;
            var listaBlocos = obterListaBlocosGenericosPorFerramenta(blocoExplosivo.FerramentaExplosiva, blocoExplosivo.gameObject, UnityEngine.Vector3.up);
            interacaoFerramentaComBloco(blocoExplosivo.FerramentaExplosiva, listaBlocos);
        }

        return resultadoDano;
    }
}
