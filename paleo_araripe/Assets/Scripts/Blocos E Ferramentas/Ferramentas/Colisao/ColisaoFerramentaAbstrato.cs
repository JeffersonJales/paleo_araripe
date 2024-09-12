using System.Collections.Generic;
using UnityEngine;

public abstract class ColisaoFerramentaAbstrato
{
    public abstract List<GameObject> obterBlocos(GameObject pontoInicial, Vector3 normal);
    protected int obterMascaraColisao()
    {
        return LayerMask.NameToLayer("BlocoArqueologico");
    }

    protected List<GameObject> compactarCollidersEmGameObjects(Collider[] colliders)
    {
        List <GameObject> lista = new List<GameObject>();
        foreach(Collider collider in colliders)
        {
            lista.Add(collider.gameObject);
        }

        return lista;
    }

    protected List<GameObject> adicionarMaisCollidersNaLista(Collider[] colliders, List<GameObject> lista)
    {
        foreach (Collider collider in colliders)
        {
            lista.Add(collider.gameObject);
        }

        return lista;
    }
    
    //    Collider[] colliders = Physics.OverlapBox(blocoAlvoRaycast.transform.position, area / 2);
}
