using System.Collections.Generic;
using UnityEngine;

public class ColisaoFerramentaPonto : ColisaoFerramentaAbstrato
{
    public override List<GameObject> obterBlocos(GameObject blocoInicial, Vector3 normal)
    {
        return new List<GameObject>() { blocoInicial };
    }
}
