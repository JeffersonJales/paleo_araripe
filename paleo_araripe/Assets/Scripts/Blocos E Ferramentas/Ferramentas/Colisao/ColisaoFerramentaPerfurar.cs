using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe {
    /// <summary>
    /// Classe de colisão para ferramentas do tipo perfurar, encontra blocos em uma linha na direção da normal.
    /// </summary>
    public class ColisaoFerramentaPerfurar : ColisaoFerramentaAbstrato
    {
        public override List<GameObject> obterBlocos(GameObject pontoInicial, Vector3 normal) { 
            Collider[] colliders = colisoes.colisaoCubica(pontoInicial.transform.position - normal, new Vector3(0, 0, 1), Quaternion.LookRotation(normal));
            return compactarCollidersEmGameObjects(colliders);
        }
    }
}
