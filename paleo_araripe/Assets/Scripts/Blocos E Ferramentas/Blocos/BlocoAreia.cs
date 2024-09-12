using System;
using UnityEngine;
using Utilidades;

public class BlocoAreia : BlocoGenerico
{
    private float tamanhoRaycastPraBaixo = 20f;
    private ColisoesBlocosChao col = new ColisoesBlocosChao();
    private Boolean jaEstaNoChao = false;
    
    // Getters
    public bool JaEstaNoChao => jaEstaNoChao; 

    
    public void Start()
    {
        jaEstaNoChao = col.checarCuboEstaNoChao(gameObject);
    }

    
    private Boolean checarBlocoAbaixo()
    {
        return col.colisaoPonto(transform.position + Vector3.down).Length > 0;
    }

    public Boolean cair()
    {
        if (jaEstaNoChao || checarBlocoAbaixo())
            return false;

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, tamanhoRaycastPraBaixo, col.obterMascaraBlocoArqueologico()))
        {
            transform.position = hit.collider.gameObject.transform.position + Vector3.up;
        }
        else {
            if (Physics.Raycast(ray, out hit, tamanhoRaycastPraBaixo, col.obterMascaraChao()))
                transform.position = new Vector3(transform.position.x, hit.collider.gameObject.transform.position.y + 0.5f, transform.position.z);

            jaEstaNoChao = true;
        }
     
        return true;
    }
}
