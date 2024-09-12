using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisaoFerramentaCamera : ColisaoFerramentaAbstrato
{
    public override List<GameObject> obterBlocos(GameObject pontoInicial, Vector3 normal)
    {
        Vector3 posicalInicial = pontoInicial.transform.position;
        normal = normal.normalized;

        Vector3 rightDirection = Vector3.Cross(normal, Vector3.up);
        Vector3 upDirection = Vector3.Cross(normal, rightDirection);
        
        if (rightDirection == Vector3.zero)
            rightDirection = Vector3.forward; // Alternativa para faces planas em cima ou embaixo

        if (upDirection == Vector3.zero)
            upDirection = Vector3.up;

        Vector3 leftDirection = -rightDirection;
        Vector3 downDirection = -upDirection;



        Collider[] colliders = Physics.OverlapBox(posicalInicial + rightDirection, Vector3.zero);
        Collider[] colliders2 = Physics.OverlapBox(posicalInicial + leftDirection, Vector3.zero);
        Collider[] colliders3 = Physics.OverlapBox(posicalInicial + upDirection, Vector3.zero);
        Collider[] colliders4 = Physics.OverlapBox(posicalInicial + downDirection, Vector3.zero);

        List<GameObject> lista = compactarCollidersEmGameObjects(colliders);
        lista = adicionarMaisCollidersNaLista(colliders2, lista);
        lista = adicionarMaisCollidersNaLista(colliders3, lista);
        lista = adicionarMaisCollidersNaLista(colliders4, lista);

        return lista;
    }
}
