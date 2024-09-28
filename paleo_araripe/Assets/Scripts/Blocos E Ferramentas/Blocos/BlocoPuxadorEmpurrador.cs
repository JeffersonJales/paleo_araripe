using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilidades;

public class BlocoPuxadorEmpurrador : BlocoGenerico
{
    public enum EFEITO_PUXAR
    {
        CIMA, 
        BAIXO, 
        FRENTE, 
        TRAS, 
        DIREITA, 
        ESQUERDA
    }

    [SerializeField] private bool puxar = true;
    [SerializeField] List<EFEITO_PUXAR> direcaoEfeitos;

    private ColisoesBlocosChao col = new ColisoesBlocosChao();

    public override void aoSerDestruido()
    {
        bc.enabled = false;
        
        if (puxar)
            iniciarPuxao();
        else
            iniciarEmpurrao();

        base.aoSerDestruido();
    }

    #region
    
    public void iniciarPuxao()
    {
        foreach(var p in direcaoEfeitos)
        {
            switch (p)
            {
                case EFEITO_PUXAR.CIMA:     realizarPuxao(Vector3.up);          break;
                case EFEITO_PUXAR.BAIXO:    realizarPuxao(Vector3.down);        break;
                case EFEITO_PUXAR.FRENTE:   realizarPuxao(Vector3.forward);     break;
                case EFEITO_PUXAR.TRAS:     realizarPuxao(Vector3.back);        break;
                case EFEITO_PUXAR.DIREITA:  realizarPuxao(Vector3.right);       break;
                case EFEITO_PUXAR.ESQUERDA: realizarPuxao(Vector3.left);        break;
            }
        }
    }

    private void realizarPuxao(Vector3 direcao){
        realizarPuxao(transform.position, direcao);
    }

    private void realizarPuxao(Vector3 posicao, Vector3 direcao)
    {
        GameObject blocoPuxado = col.obterBlocoPorRaycast(transform.position, direcao);
        if(blocoPuxado != null)
        {
            var mr = blocoPuxado.GetComponent<BoxCollider>();
            mr.enabled = false;

            blocoPuxado.transform.position = posicao + direcao * UtilitariosGamePlay.UNIDADE_TAMANHO_CUBO;
            
            realizarPuxao(blocoPuxado.transform.position, direcao);
            mr.enabled = true;
        }
    }
    
    #endregion puxador

    private void iniciarEmpurrao()
    {

    }
}
