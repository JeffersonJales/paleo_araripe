using System.Collections.Generic;
using UnityEngine;

public class ColisaoFerramentaPonto : ColisaoFerramentaAbstrato
{
    public override List<GameObject> obterBlocos(GameObject blocoInicial)
    {
        return new List<GameObject>() { blocoInicial };
    }
}
