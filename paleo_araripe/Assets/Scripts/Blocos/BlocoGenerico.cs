using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoGenerico : MonoBehaviour
{
    [SerializeField] private int vidaAtual = 1; 
    [SerializeField] private BlocosSO blocoSO;


    private Boolean emFoco = false;
    private MeshRenderer mr = null;
    public BlocosSO BlocoSO => blocoSO; 

    public void Start()
    {
        vidaAtual = BlocoSO.Vida;
        mr = GetComponent<MeshRenderer>();
        mr.material = blocoSO.CorMaterialNaoDestacado;
    }

    public Boolean interagir()
    {
        Boolean destruido = false;
        
        if (--vidaAtual <= 0) {
            destruido = true;
            destruirBloco();
        }

        return destruido;
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

        mr.material = blocoSO.CorMaterialNaoDestacado;
    }

    public void casoDeixeDeSerFocoDaFerramenta()
    {
        if(!emFoco) return;
        emFoco = false;

        mr.material = blocoSO.CorMaterialNaoDestacado;
    }
}
