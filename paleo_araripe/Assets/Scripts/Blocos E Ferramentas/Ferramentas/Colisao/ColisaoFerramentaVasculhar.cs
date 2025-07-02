using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe {
    /// <summary>
    /// Classe de colisão para ferramentas do tipo vasculhar, encontra blocos em uma área cúbica ao redor do ponto inicial.
    /// </summary>
    public class ColisaoFerramentaVasculhar : ColisaoFerramentaAbstrato
    {
        public override List<GameObject> obterBlocos(GameObject pontoInicial, Vector3 normal)
        {
            Collider[] colliders = colisoes.colisaoCubica(pontoInicial.transform.position, new Vector3(1, 1, 1), Quaternion.LookRotation(normal));
            return compactarCollidersEmGameObjects(colliders);
        }
    }
}
