using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisaoFerramentaExplosivo : ColisaoFerramentaAbstrato
{
    // Start is called before the first frame update
    public override List<GameObject> obterBlocos(GameObject pontoInicial, Vector3 normal)
    {
        Collider[] colliders = colisoes.colisaoCubica(pontoInicial.transform.position + normal.normalized, Vector3.one, Quaternion.LookRotation(normal));
        return compactarCollidersEmGameObjects(colliders);
    }
}
