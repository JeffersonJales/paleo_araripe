using UnityEngine;

namespace PaleoAraripe {

/// <summary>
/// Classe que define o comportamento do bloco de calcário, incluindo efeito de fumaça ao ser destruído.
/// </summary>
public class BlocoCalcario : BlocoGenerico
{
    [SerializeField] private GameObject fumaca;
    [SerializeField] private int tempoFumacaAtiva = 2;

    private GameObject refFumaca;

    public override void aoSerDestruido()
    {
        bc.enabled = false;
        mr.enabled = false;

        refFumaca = Instantiate(fumaca, transform.position, Quaternion.identity, transform.parent);
        UtilitariosGamePlay.ouvirResumoInteracaoFerramentaBloco(passagemTempo);
    }


    private void passagemTempo(ResumoInteracaoBlocoFerramenta resumo) 
    {
        if (--tempoFumacaAtiva < 0)
            Destroy(gameObject);
    }


    public void OnDestroy()
    {
        UtilitariosGamePlay.pararOuvirResumoInteracaoFerramentaBloco(passagemTempo);

        if (refFumaca != null) 
            Destroy(refFumaca);
    }

}

}
