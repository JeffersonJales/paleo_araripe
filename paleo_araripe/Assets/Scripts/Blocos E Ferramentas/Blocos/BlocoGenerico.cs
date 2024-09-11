using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoGenerico : MonoBehaviour
{
    [SerializeField] private int vidaAtual = 1; 
    [SerializeField] private BlocoSO blocoSO;


    private Boolean emFoco = false;
    private MeshRenderer mr = null;
    public BlocoSO BlocoSO => blocoSO; 

    public void Start()
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

    public void serColetado()
    {
        destruirBloco();
    }

    public void destruirBloco()
    {
        Destroy(gameObject);
    }


    // @TODO FSM
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
}
