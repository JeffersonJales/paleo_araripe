using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using static PlasticPipe.PlasticProtocol.Messages.Serialization.ItemHandlerMessagesSerialization;

public class InteracaoBlocoFerramenta {

    public void interacaoFerramentaComBloco(FerramentaSO ferramenta, List<BlocoGenerico> blocosGenericos, out ResumoInteracaoBlocoFerramenta resumo)
    {
        resumo = new ResumoInteracaoBlocoFerramenta();
        resumo.FerramentaUsada = ferramenta;

        foreach (BlocoGenerico bloco in blocosGenericos)
        {
            BlocosSO blocoSO = bloco.BlocoSO;
            resumo.BlocosAfetados.Add(blocoSO);

            switch (blocoSO.Tipo)
            {
                case NaturezaBlocoFerramenta.TipoBloco.NORMAL:
                    resumo.BlocosDestruidos.Add(interacaoFerramentaComBlocoNormal(ferramenta, bloco)); 
                    break;


                case NaturezaBlocoFerramenta.TipoBloco.AMBAR:
                case NaturezaBlocoFerramenta.TipoBloco.FOSSIL:
                    break;
            }
        }

    }

    private Boolean interacaoFerramentaComBlocoNormal(FerramentaSO ferramenta, BlocoGenerico bloco)
    {
        if(NaturezaBlocoFerramenta.ferramentaQuebraBloco(ferramenta, bloco.BlocoSO))
        {
            bloco.destruirBloco();
            return true;
        }

        return false;
    }
}
