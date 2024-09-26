using System;
using UnityEngine;

public class BlocoGenerico : MonoBehaviour
{
    [SerializeField] private BlocoSO blocoSO;

    private int vidaAtual = 1;
    private Boolean emFoco = false;
    protected MeshRenderer mr = null;
    protected BoxCollider bc = null;


    // Getters 
    public BlocoSO BlocoSO => blocoSO; 


    public virtual void Awake()
    {
        vidaAtual = BlocoSO.Vida;

        mr = GetComponent<MeshRenderer>();
        mr.material = blocoSO.CorMaterialNaoDestacado;

        bc = GetComponent<BoxCollider>();
    }


    public Boolean tomarDano(int qtdDano)
    {
        Boolean destruido = false;
        vidaAtual -= qtdDano;

        if (vidaAtual <= 0)
        {
            destruido = true;
            destruirBloco();
        }
        else
            aoTomarDano();

        return destruido;
    }

    public Boolean estaVivo()
    {
        return vidaAtual > 0 && bc.enabled;
    }

    public virtual void aoTomarDano()
    {

    }
   
    public virtual void destruirBloco()
    {
        bc.enabled = false;
        Destroy(gameObject);
    }

    public virtual void serColetado()
    {
        destruirBloco();
    }


    #region Feedback Bloco é Alvo da ferramenta

    public void casoSejaFocoDaFerramenta()
    {
        if (emFoco) return;
        emFoco = true;

        mr.material = blocoSO.CorMaterialDestacado;
    }

    public void casoDeixeDeSerFocoDaFerramenta()
    {
        if(!emFoco) return;
        emFoco = false;

        mr.material = blocoSO.CorMaterialNaoDestacado;
    }
    
    #endregion


    public void ouvirResumoInteracaoFerramenta(Action<ResumoInteracaoBlocoFerramenta> acao)
    {
        UsarFerramentas usoFerramenta = FindObjectOfType<UsarFerramentas>();
        if (usoFerramenta != null)
        {
            usoFerramenta.EventoAposRealizarUsoFerramenta += acao;
        }
    }

    public void pararDeOuvirInteracaoFerramenta(Action<ResumoInteracaoBlocoFerramenta> acao)
    {
        UsarFerramentas usoFerramenta = FindObjectOfType<UsarFerramentas>();
        if (usoFerramenta != null)
        {
            usoFerramenta.EventoAposRealizarUsoFerramenta -= acao;
        }
    }
}
