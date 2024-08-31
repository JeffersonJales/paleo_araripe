using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoAreia : MonoBehaviour
{
    [Range(1, 5)]
    [SerializeField] private int vida = 1; // Quantidade de vezes que deve clicar até desaparecer
    [SerializeField] private Material corMaterialNormal = null;
    [SerializeField] private Material corMaterialEmFoco = null;

    private Boolean emFoco = false;
    private MeshRenderer mr = null;
    public void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    public Boolean interagir()
    {
        Boolean destruido = false;
        
        if (--vida <= 0) {
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

        mr.material = corMaterialEmFoco;
    }

    public void casoDeixeDeSerFocoDaFerramenta()
    {
        if(!emFoco) return;
        emFoco = false;

        mr.material = corMaterialNormal;
    }
}
