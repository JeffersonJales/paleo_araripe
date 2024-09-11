using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor.SceneManagement;

public class TrocarFerramentaViaBotaoUI : MonoBehaviour
{
    [SerializeField] private FerramentaSO tipoFerramenta;

    private Button botao;
    private Image spriteFerramenta;
    private TextMeshProUGUI textMeshContadorTempo;

    private Boolean emContragemRegressiva = false;
    private Boolean esperandoInspiracao = false;

    private int contadorTempo = 0;
    private int inspiracaoNecessariaUso = 0;

    public Action<FerramentaSO> aoPressionarBotao;

    public void Start()
    {
        Boolean temContagemRegressiva = tipoFerramenta.ContagemRegressivaParaReuso > 0;
        Boolean necessitaInspiracao = tipoFerramenta.Inspiracao < 0;

        botao = GetComponent<Button>();
        botao.onClick.AddListener(realizarMudancaFerramenta);

        textMeshContadorTempo = GetComponentInChildren<TextMeshProUGUI>();
        textMeshContadorTempo.enabled = false;
        
        spriteFerramenta = GetComponentInChildren<Image>();
        spriteFerramenta.sprite = tipoFerramenta.SpriteFerramenta;

        var usarFerramenta = FindObjectOfType<UsarFerramentas>();
        if(usarFerramenta != null)
        {
            aoPressionarBotao += usarFerramenta.trocarFerramenta;

            if (necessitaInspiracao) {
                inspiracaoNecessariaUso = Math.Abs(tipoFerramenta.Inspiracao);
                usarFerramenta.InspiracaoAlterada += aoAlterarInspiracao;
            }

            if (temContagemRegressiva)
                usarFerramenta.EventoResumoInteracao += aoUtilizarFerramenta;
        }
    }

    public void realizarMudancaFerramenta()
    {
        aoPressionarBotao?.Invoke(tipoFerramenta);
    }

    public void aoUtilizarFerramenta(ResumoInteracaoBlocoFerramenta resumo)
    {
        if (resumo.FerramentaUsada.Equals(tipoFerramenta))
            iniciarContagemRegressivaFerramenta();
        else
            diminuirContagemRegressiva(resumo.FerramentaUsada);
    }

    private void iniciarContagemRegressivaFerramenta()
    {
        contadorTempo = tipoFerramenta.ContagemRegressivaParaReuso;
        textMeshContadorTempo.enabled = true;
        textMeshContadorTempo.text = contadorTempo.ToString();
        spriteFerramenta.color = new Color(1, 1, 1, 0.5f);
        botao.interactable = false;
        aoPressionarBotao?.Invoke(null);
    }

    private void finalizarContagemRegressiva()
    {
        contadorTempo = 0;
        spriteFerramenta.color = new Color(1, 1, 1, 1);
        textMeshContadorTempo.enabled = false;
        botao.interactable = true;
    }

    private void diminuirContagemRegressiva(FerramentaSO ferramenta)
    {
        if(--contadorTempo <= 0)
            finalizarContagemRegressiva();

        textMeshContadorTempo.text = contadorTempo.ToString();
    }

    private void aoAlterarInspiracao(int atual, int maximo)
    {
        Boolean estadoBotaoInspiracao = inspiracaoNecessariaUso <= atual;
        if (esperandoInspiracao != estadoBotaoInspiracao)
            esperandoInspiracao = estadoBotaoInspiracao;
    }
}
