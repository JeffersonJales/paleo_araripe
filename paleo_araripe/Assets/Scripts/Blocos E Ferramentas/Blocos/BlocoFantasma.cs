
using System;
using UnityEngine;
using Utilidades;

public class BlocoFantasma : BlocoGenerico
{

    [SerializeField] private int turnosParaReativar = 3;
    private int turnosDesativados = 0;

    public void OnDestroy()
    {
        pararDeOuvirInteracaoFerramenta(aoUtilizarFerramenta);
    }


    public override void destruirBloco()
    {
        bc.enabled = false;
        mr.enabled = false;
        turnosDesativados = 0;
        ouvirResumoInteracaoFerramenta(aoUtilizarFerramenta);

    }

    public override void serColetado()
    {
        destruirBloco();
    }

    private void reativarBloco()
    {
        if(new ColisoesBlocosChao().checarSeEspacoEstaOcupado(transform.position)) {
            Destroy(gameObject);
        }
        else
        {
            mr.enabled = true;
            bc.enabled = true;
            pararDeOuvirInteracaoFerramenta(aoUtilizarFerramenta);
        }

    }

    private void aoUtilizarFerramenta(ResumoInteracaoBlocoFerramenta resumo)
    {
        if (++turnosDesativados > turnosParaReativar)
            reativarBloco();
    }

}
