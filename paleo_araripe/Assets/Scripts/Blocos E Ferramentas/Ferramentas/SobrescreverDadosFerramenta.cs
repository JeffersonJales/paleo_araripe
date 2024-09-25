using System.IO;
using UnityEditor;
using UnityEngine;

public class SobrescreverDadosFerramenta : MonoBehaviour
{   

    [HideInInspector] public FerramentaSO novaFerramenta;
    [HideInInspector] public FerramentaSO ferramentaOriginal;

    private TrocarFerramentaViaBotaoUI trocaFerramenta;

    public void Start()
    {
        trocaFerramenta = GetComponent<TrocarFerramentaViaBotaoUI>();
        if (trocaFerramenta != null)
        {
            ferramentaOriginal = trocaFerramenta.TipoFerramenta;
            novaFerramenta = Instantiate(ferramentaOriginal);
        }
        else
            novaFerramenta = ScriptableObject.CreateInstance<FerramentaSO>();
    }

    public void OnDestroy()
    {
        Destroy(novaFerramenta);
    }


    public void aplicarSobrescrita()
    {
        if(trocaFerramenta != null)
            trocaFerramenta.TipoFerramenta = novaFerramenta;
    }

    public void reverterModificacoes()
    {
        if (trocaFerramenta != null)
            trocaFerramenta.TipoFerramenta = ferramentaOriginal;
    }

    public void salvarFerramentaNova(string caminho)
    {
        if(novaFerramenta != null)
        {
            AssetDatabase.CreateAsset(novaFerramenta, caminho);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("ScriptableObject salvo em: " + caminho);
        }
    }
}
