using System;
using UnityEngine;
using Utilidades;

public class BlocoDesorganizador : BlocoGenerico
{
    private ColisoesBlocosChao col = new ColisoesBlocosChao();

    public override void aoSerDestruido()
    {
        desorganizarBlocos();
        base.aoSerDestruido();
    }

    private void desorganizarBlocos()
    {
        Boolean blocoEstaNoChao = col.checarCuboEstaNoChao(gameObject);
        executarTelefrag(Vector3.up, true, blocoEstaNoChao);

        executarTelefrag(Vector3.right, false, blocoEstaNoChao);
        executarTelefrag(Vector3.forward, false, blocoEstaNoChao);

        executarTelefrag(Vector3.forward + Vector3.right, false, blocoEstaNoChao);
        executarTelefrag(Vector3.forward + Vector3.left, false, blocoEstaNoChao);

        executarTelefrag(Vector3.up + Vector3.right, true, blocoEstaNoChao);
        executarTelefrag(Vector3.up + Vector3.left, true, blocoEstaNoChao);

        executarTelefrag(Vector3.up + Vector3.forward, true, blocoEstaNoChao);
        executarTelefrag(Vector3.up + Vector3.back, true, blocoEstaNoChao);

        executarTelefrag(Vector3.up + Vector3.forward + Vector3.right, true, blocoEstaNoChao);
        executarTelefrag(Vector3.up + Vector3.forward + Vector3.left, true, blocoEstaNoChao);
        
        executarTelefrag(Vector3.up + Vector3.back + Vector3.right, true, blocoEstaNoChao);
        executarTelefrag(Vector3.up + Vector3.back + Vector3.left, true, blocoEstaNoChao);
    }

    private void executarTelefrag(Vector3 direcao, Boolean chaoImporta, Boolean estaNoChao)
    {
        if(chaoImporta && estaNoChao)
            return;

        float distBlocos = UtilitariosGamePlay.UNIDADE_TAMANHO_CUBO;
        float distReflexao = distBlocos * 2;


        var blocoDirecao = col.pegarPrimeiroObjeto(col.colisaoPonto(transform.position + direcao * distBlocos));
        var blocoOposto = col.pegarPrimeiroObjeto(col.colisaoPonto(transform.position - direcao * distBlocos));

        if (blocoDirecao != null)
            blocoDirecao.transform.position += -direcao * distReflexao;

        if (blocoOposto != null)
            blocoOposto.transform.position += direcao * distReflexao;
    }
}
