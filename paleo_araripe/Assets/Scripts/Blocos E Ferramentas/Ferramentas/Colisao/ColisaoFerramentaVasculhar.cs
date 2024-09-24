using System.Collections.Generic;
using UnityEngine;

public class ColisaoFerramentaVasculhar : ColisaoFerramentaAbstrato
{
    public override List<GameObject> obterBlocos(GameObject pontoInicial, Vector3 normal)
    {
        Collider[] colliders = colisoes.colisaoCubica(pontoInicial.transform.position, new Vector3(1, 1, 0), Quaternion.LookRotation(normal));
        return compactarCollidersEmGameObjects(colliders);
    }
}
