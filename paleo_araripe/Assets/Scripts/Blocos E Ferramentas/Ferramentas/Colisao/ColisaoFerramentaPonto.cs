using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe {
    /// <summary>
    /// Classe de colisão para ferramentas do tipo ponto, retorna apenas o bloco inicial como alvo.
    /// </summary>
    public class ColisaoFerramentaPonto : ColisaoFerramentaAbstrato
    {
        public override List<GameObject> obterBlocos(GameObject blocoInicial, Vector3 normal)
        {
            return new List<GameObject>() { blocoInicial };
        }
    }
}
