using UnityEngine;
using Utilidades;

public class BlocoLava : BlocoGenerico
{

    [SerializeField] private GameObject refLavaLiquida;
    private int distanciaMaxima = 10;

    public override void aoSerDestruido()
    {
        participarColisao(false);
        instanciarBlocoLiquido(new ColisoesBlocosChao(), gameObject, 0);
        
        Destroy(gameObject);
    }

    private void instanciarBlocoLiquido(ColisoesBlocosChao col, GameObject objetoAnterior, int colisoes)
    {
        if (!col.checarCuboEstaNoChao(objetoAnterior) && ++colisoes < distanciaMaxima)
        {
            Vector3 posicaoProximoBloco = objetoAnterior.transform.position + Vector3.down * UtilitariosGamePlay.UNIDADE_TAMANHO_CUBO;

            var blocoAbaixo = col.colisaoPonto(posicaoProximoBloco);

            if (blocoAbaixo.Length.Equals(0))
                instanciarBlocoLiquido(col, Instantiate(refLavaLiquida, posicaoProximoBloco, Quaternion.identity, objetoAnterior.transform.parent), colisoes);
        }
    }
}
