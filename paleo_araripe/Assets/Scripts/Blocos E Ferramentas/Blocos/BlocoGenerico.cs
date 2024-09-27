using System;
using UnityEngine;

public class BlocoGenerico : MonoBehaviour
{
    [SerializeField] private BlocoSO blocoSO;

    private int vidaAtual = 1;
    private Boolean emFoco = false;
    private Boolean emFocoCristal = false;
    
    protected MeshRenderer mr = null;
    protected BoxCollider bc = null;

    // Getters 
    public BlocoSO BlocoSO => blocoSO; 


    public virtual void Awake()
    {
        vidaAtual = BlocoSO.Vida;

        bc = GetComponent<BoxCollider>();
    
        mr = GetComponent<MeshRenderer>();
        mr.material = blocoSO.CorMaterialNaoDestacado;
    }


    public Boolean tomarDano(int qtdDano)
    {
        Boolean destruido = false;
        vidaAtual -= qtdDano;

        if (vidaAtual <= 0)
        {
            destruido = true;
            aoSerDestruido();
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
   
    public virtual void aoSerDestruido()
    {
        bc.enabled = false;
        Destroy(gameObject);
    }

    public virtual void aoSerColetado()
    {
        aoSerDestruido();
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

    #region Feedback Bloco alvo do cristal

    public void casoSejaFocoDoCristal()
    {
        emFocoCristal = true;
    }

    public void casoDeixeDeSerFocoDoCristal()
    {
        emFocoCristal = false;
    }

    #endregion
}
