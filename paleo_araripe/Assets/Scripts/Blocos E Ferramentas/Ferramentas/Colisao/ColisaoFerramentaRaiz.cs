using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe {
    /// <summary>
    /// Classe de colisão para ferramentas do tipo raiz, encontra blocos dois níveis abaixo do ponto inicial.
    /// </summary>
    public class ColisaoFerramentaRaiz : ColisaoFerramentaAbstrato
    {
        public override List<GameObject> obterBlocos(GameObject pontoInicial, Vector3 normal)
        {
            Vector3 posicao = pontoInicial.transform.position + (Vector3.down * 2 * UtilitariosGamePlay.UNIDADE_TAMANHO_CUBO); // Dois blocos para baixo
            Collider[] colliders = colisoes.colisaoCubica(posicao, new Vector3(0.1f, 1f, 1f), Quaternion.identity);
            return compactarCollidersEmGameObjects(colliders);
        }
    }
}
