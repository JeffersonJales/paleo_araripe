using log4net.Util;
using System;
using UnityEngine;
using Utilidades;
using static Codice.Client.Commands.WkTree.WorkspaceTreeNode;

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

        if (!verificarBlocoAbaixo())
            cairAteChao();
     
        return true;
    }

    private Boolean verificarBlocoAbaixo()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, tamanhoRaycastPraBaixo, col.obterMascaraBlocoArqueologico())){
            var blocInfo = hit.collider.gameObject.GetComponent<BlocoGenerico>();
            if (blocInfo.BlocoSO.SofreDanoQuandoCuboCaiNele && blocInfo.tomarDano(1))
            {
                return verificarBlocoAbaixo();
            }
            else 
            { 
                transform.position = hit.collider.gameObject.transform.position + Vector3.up;
            }

            return true;
        }


        return false;
    }

    private void cairAteChao()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, tamanhoRaycastPraBaixo, col.obterMascaraChao()))
            transform.position = new Vector3(transform.position.x, hit.collider.gameObject.transform.position.y + 0.5f, transform.position.z);

        jaEstaNoChao = true;
    }
}
