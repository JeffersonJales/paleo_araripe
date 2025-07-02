using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe {
    /// <summary>
    /// Classe abstrata base para colisão de ferramentas, define a interface e utilitários para encontrar blocos afetados por uma ferramenta.
    /// </summary>
    public abstract class ColisaoFerramentaAbstrato
    {
        public abstract List<GameObject> obterBlocos(GameObject pontoInicial, Vector3 normal);

        
        protected ColisoesBlocosChao colisoes = new ColisoesBlocosChao();

        protected List<GameObject> compactarCollidersEmGameObjects(Collider[] colliders)
        {
            List <GameObject> lista = new List<GameObject>();
            foreach(Collider collider in colliders)
            {
                lista.Add(collider.transform.parent.gameObject);
            }

            return lista;
        }

        protected List<GameObject> adicionarMaisCollidersNaLista(Collider[] colliders, List<GameObject> lista)
        {
            foreach (Collider collider in colliders)
            {
                lista.Add(collider.transform.parent.gameObject);
            }

            return lista;
        }
    }
}
