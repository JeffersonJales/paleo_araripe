using System;
using System.Collections.Generic;
using UnityEngine;

public class BlocoCristral : BlocoGenerico
{
    [SerializeField] private int turnosMostrandoObjetivos = 1;
    [SerializeField] private BlocoSO refBlocoFossil;
    [SerializeField] private List<BlocoGenerico> blocosAlvo;
    
    private int quantidadeTurnosRestantes = 0;
    private Boolean ativado = false;

    public override void aoSerDestruido()
    {
        ativado = true;
        bc.enabled = false;
        mr.enabled = false;
        apresentarBlocos();
    }

    public void OnDestroy()
    {
        UtilitariosGamePlay.pararOuvirResumoInteracaoFerramentaBloco(passagemTempo);
    }

    private void apresentarBlocos()
    {
        blocosAlvo = UtilitariosGamePlay.obterBlocos(refBlocoFossil);
        if (blocosAlvo.Count.Equals(0))
        {
            enfimSerDestruido();
            return;
        }
        else
        {
            foreach(var bloco in blocosAlvo)
            {
                bloco.casoSejaFocoDoCristal();
            }
        }

        UtilitariosGamePlay.ouvirResumoInteracaoFerramentaBloco(passagemTempo);
        quantidadeTurnosRestantes = turnosMostrandoObjetivos;
    }


    private void pararApresentarBlocos()
    {
        blocosAlvo = UtilitariosGamePlay.obterBlocos(refBlocoFossil);
        foreach (var bloco in blocosAlvo)
        {
            bloco.casoDeixeDeSerFocoDoCristal();
        }

        enfimSerDestruido();
    }

    private void passagemTempo(ResumoInteracaoBlocoFerramenta resumo)
    {
        if (--quantidadeTurnosRestantes < 0)
            pararApresentarBlocos();
    }

    private void enfimSerDestruido()
    {
        Destroy(gameObject);
    }
}
