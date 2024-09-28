using System;
using System.Collections.Generic;
using UnityEngine;

public class UtilitariosGamePlay : MonoBehaviour
{

    public static readonly float UNIDADE_TAMANHO_CUBO = 1;
    public static readonly float UNIDADE_METADE_TAMANHO_CUBO = 0.5f;


    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Não utilizar isso no Awake
    public static void ouvirResumoInteracaoFerramentaBloco(Action<ResumoInteracaoBlocoFerramenta> acao)
    {
        UsarFerramentas usoFerramenta = FindObjectOfType<UsarFerramentas>();
        if (usoFerramenta != null)
        {
            usoFerramenta.EventoAposRealizarUsoFerramenta += acao;
        }
    }
    
    public static void pararOuvirResumoInteracaoFerramentaBloco(Action<ResumoInteracaoBlocoFerramenta> acao)
    {
        UsarFerramentas usoFerramenta = FindObjectOfType<UsarFerramentas>();
        if (usoFerramenta != null)
        {
            usoFerramenta.EventoAposRealizarUsoFerramenta -= acao;
        }
    }

    public static List<GameObject> obterBlocosGameObjects()
    {
        var listaGameObject = new List<GameObject>(); 
        foreach(var bloco in FindObjectsOfType<BlocoGenerico>())
        {
            listaGameObject.Add(bloco.gameObject);
        }

        return listaGameObject;
    }

    public static BlocoGenerico[] obterBlocos()
    {
        return FindObjectsOfType<BlocoGenerico>();
    }

    public static List<BlocoGenerico> obterBlocos(NaturezaBlocoFerramenta.IdentificadorBloco identificadorUnico)
    {
        List<BlocoGenerico> blocos = new List<BlocoGenerico>(); 
        foreach(var i in obterBlocos())
        {
            if (i.BlocoSO.Identificador.Equals(identificadorUnico))
            {
                blocos.Add(i);
            }
        }

        return blocos;
    }

    public static List<BlocoGenerico> obterBlocos(BlocoSO blocoSO)
    {
        List<BlocoGenerico> blocos = new List<BlocoGenerico>();
        foreach (var i in obterBlocos())
        {
            if (i.BlocoSO.Equals(blocoSO)) 
            {
                blocos.Add(i);
            }
        }

        return blocos;
    }

    public static T[] obterBlocos<T>() where T : BlocoGenerico
    {
        return FindObjectsOfType<T>();
    }


}
