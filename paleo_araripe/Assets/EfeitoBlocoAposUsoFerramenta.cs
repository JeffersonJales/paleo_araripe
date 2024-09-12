using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoBlocoAposUsoFerramenta : MonoBehaviour
{
    void Start()
    {
        UsarFerramentas usarFerramentas = FindObjectOfType<UsarFerramentas>();
        if (usarFerramentas != null)
            usarFerramentas.EventoResumoInteracao += atualizarPosicaoBlocosAposInteracao;
    }

    private void atualizarPosicaoBlocosAposInteracao(ResumoInteracaoBlocoFerramenta resumo)
    {
        if (resumo.AlgumBlocoDestruidoOuColeado)
           StartCoroutine(ExecutarAposSegundos(0.1f));
    }

    private void comportamentoBlocosAreia()
    {
        Debug.Log("Tentar derrubar blocos");

        BlocoAreia[] blocosAreia = FindObjectsOfType<BlocoAreia>();
        if (blocosAreia.Length == 0)
            return;

        List<BlocoAreia> blocosPodemCair = new List<BlocoAreia>();
        List<BlocoAreia> blocosNaoCairam = new List<BlocoAreia>();

        /// Pegar blocos de areia que podem cair
        foreach (var i in blocosAreia)
        {
            if (!i.JaEstaNoChao)
                blocosPodemCair.Add(i);
        }

        /// Fazer os que podem cair, iniciar queda
        foreach (var i in blocosPodemCair)
        {
            if (!i.cair())
                blocosNaoCairam.Add(i);
        }

        if (blocosPodemCair.Count != blocosNaoCairam.Count)
           StartCoroutine(ExecutarAposSegundos(0.1f));
    }

    IEnumerator ExecutarAposSegundos(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        comportamentoBlocosAreia();
    }

}
