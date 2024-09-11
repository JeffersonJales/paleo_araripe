using System.Collections.Generic;
using UnityEngine;

public abstract class ColisaoFerramentaAbstrato
{
    public abstract List<GameObject> obterBlocos(GameObject pontoInicial);
    //    Collider[] colliders = Physics.OverlapBox(blocoAlvoRaycast.transform.position, area / 2);
}
