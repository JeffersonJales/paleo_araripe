using System.Collections.Generic;
using UnityEngine;

public class ColisaoFerramentaPerfurar : ColisaoFerramentaAbstrato
{
    public override List<GameObject> obterBlocos(GameObject pontoInicial, Vector3 normal)
    {
        Vector3 posicalReal = pontoInicial.transform.position - normal;
        Collider[] colliders = Physics.OverlapBox(posicalReal, new Vector3(0, 0, 1), Quaternion.LookRotation(normal));
        return compactarCollidersEmGameObjects(colliders);
    }
}
