using System;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe {
    /// <summary>
    /// Classe utilitária que define os tipos de blocos, resultados de interação e métodos para obter alvos de ferramentas.
    /// </summary>
    public class NaturezaBlocoFerramenta 
    { 
        public enum TipoBloco
        {
            NORMAL,
            AMBAR, 
            FOSSIL,
            INSPIRACAO,
            EXPLOSIVO,
            GELO,
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
            CAMERA,
            EXPLOSAO,
            EXPLOSIVO,
            RAIZ,
            LUPA
        }

        public enum TipoSomAoSerDestruido
        {
            NULO,
            ARENOSO,
            PEDROSO,
            VIDROSO,
            EXPLOSIVO
        }


        public static Boolean interacaoPodeResultarNaDestruicaoDoBloco(ResultadoInteracao resultado) 
        {
            return resultado == ResultadoInteracao.DESTRUIDO || resultado == ResultadoInteracao.COLETADO;
        }

        public static List<GameObject> obterListaBlocosPorFerramenta(FerramentaSO ferramenta, GameObject blocoFoco, Vector3 normal)
        {
            switch (ferramenta.TipoColisao)
            {
                case TipoColisaoFerramenta.LUPA: 
                case TipoColisaoFerramenta.PONTO:       return new ColisaoFerramentaPonto().obterBlocos(blocoFoco, normal);
                
                case TipoColisaoFerramenta.ESCOVAR:     return new ColisaoFerramentaEscovar().obterBlocos(blocoFoco, normal);
                case TipoColisaoFerramenta.PERFURAR:    return new ColisaoFerramentaPerfurar().obterBlocos(blocoFoco, normal);
                case TipoColisaoFerramenta.VASCULHAR:   return new ColisaoFerramentaVasculhar().obterBlocos(blocoFoco, normal);
                case TipoColisaoFerramenta.CAMERA:      return new ColisaoFerramentaCamera().obterBlocos(blocoFoco, normal);
                case TipoColisaoFerramenta.EXPLOSAO:    return new ColisaoFerramentaExplosao().obterBlocos(blocoFoco, normal);
                case TipoColisaoFerramenta.EXPLOSIVO:   return new ColisaoFerramentaExplosivo().obterBlocos(blocoFoco, normal);
                case TipoColisaoFerramenta.RAIZ:        return new ColisaoFerramentaRaiz().obterBlocos(blocoFoco, normal);

                case TipoColisaoFerramenta.NULO:
                default:
                    return new List<GameObject>();
            }
        }

        public static List<BlocoGenerico> obterListaBlocosGenericosPorFerramenta(FerramentaSO ferramenta, GameObject blocoFoco, Vector3 normal)
        {
            List<GameObject> objetosAlvo = obterListaBlocosPorFerramenta(ferramenta, blocoFoco, normal);
        
            List<BlocoGenerico> blocosGenericos = new List<BlocoGenerico>();
            foreach (var alvo in objetosAlvo)
            {
                blocosGenericos.Add(alvo.GetComponent<BlocoGenerico>());
            }

            return blocosGenericos;
        }

    }
}
