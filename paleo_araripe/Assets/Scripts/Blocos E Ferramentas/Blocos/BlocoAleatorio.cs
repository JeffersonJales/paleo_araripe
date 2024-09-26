using System.Collections.Generic;
using UnityEngine;

public class BlocoAleatorio : BlocoGenerico
{
    [SerializeField] private List<GameObject> blocosPossiveis = new List<GameObject>();
    [SerializeField] private GameObject blocoAtual;

    private int posicaoAtual = 0;

    public override void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void Start()
    {
        ouvirResumoInteracaoFerramenta(modificarBloco);

        if (blocosPossiveis.Count > 0)
            instanciarBloco();
        else
            Destroy(gameObject);
    }

    public void OnDestroy()
    {
        pararDeOuvirInteracaoFerramenta(modificarBloco);
        if(blocoAtual != null)
            Destroy(blocoAtual);
    }

    private void modificarBloco(ResumoInteracaoBlocoFerramenta resumo)
    {
        if (resumo.BlocosDestruidos.Contains(blocoAtual))
            Destroy(gameObject);
        else
            instanciarBloco();
    }

    private void instanciarBloco()
    {
        
        if (blocoAtual != null) { 
            transform.position = blocoAtual.transform.position;
            Destroy(blocoAtual);
        }

        blocoAtual = Instantiate(blocosPossiveis[posicaoAtual], transform.position, transform.rotation);

        if (++posicaoAtual >= blocosPossiveis.Count)
            posicaoAtual = 0;
    }

}
