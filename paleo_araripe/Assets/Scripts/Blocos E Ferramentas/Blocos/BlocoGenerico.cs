using System;
using UnityEngine;

public class BlocoGenerico : MonoBehaviour
{
    [SerializeField] private BlocoSO blocoSO;

    private int vidaAtual = 1;
    private Boolean emFoco = false;
    private MeshRenderer mr = null;

    // Getters 
    public BlocoSO BlocoSO => blocoSO; 


    public void Awake()
    {
        vidaAtual = BlocoSO.Vida;
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
            destruirBloco();
        }

        return destruido;
    }
   
    public void destruirBloco()
    {
        Destroy(gameObject);
    }

    public void serColetado()
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

}
