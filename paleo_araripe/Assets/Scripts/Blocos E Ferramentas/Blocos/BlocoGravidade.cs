using Utilidades;
using UnityEngine;

public class BlocoGravidade : BlocoGenerico
{
    private ColisoesBlocosChao col = new ColisoesBlocosChao();
    
    public override void aoSerDestruido()
    {
        participarColisao(false);
        proximoBlocoCaiSobOutroBloco(gameObject, true);
        base.aoSerDestruido();
    }

    private void proximoBlocoCaiSobOutroBloco(GameObject obj, bool causaDano)
    {
        var proximoObj = col.obterBlocoPorRaycast(obj.transform.position + new Vector3(0, UtilitariosGamePlay.UNIDADE_TAMANHO_CUBO, 0), Vector3.up);
        if (proximoObj != null)
        {
            col.cairSobOutroCubo(proximoObj, causaDano);
            proximoBlocoCaiSobOutroBloco(proximoObj, false);
        }
    }
}
