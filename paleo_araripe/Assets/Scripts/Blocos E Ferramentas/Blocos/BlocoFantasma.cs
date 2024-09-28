
using System;
using UnityEngine;
using Utilidades;

public class BlocoFantasma : BlocoGenerico
{

    [SerializeField] private int turnosParaReativar = 3;
    private int turnosDesativados = 0;

    public void OnDestroy()
    {
        UtilitariosGamePlay.pararOuvirResumoInteracaoFerramentaBloco(aoUtilizarFerramenta);
    }


    public override void aoSerDestruido()
    {
        bc.enabled = false;
        mr.enabled = false;
        turnosDesativados = 0;
        UtilitariosGamePlay.ouvirResumoInteracaoFerramentaBloco(aoUtilizarFerramenta);
    }

    public override void aoSerColetado()
    {
        aoSerDestruido();
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
            UtilitariosGamePlay.pararOuvirResumoInteracaoFerramentaBloco(aoUtilizarFerramenta);
        }

    }

    private void aoUtilizarFerramenta(ResumoInteracaoBlocoFerramenta resumo)
    {
        if (++turnosDesativados > turnosParaReativar)
            reativarBloco();
    }

}
