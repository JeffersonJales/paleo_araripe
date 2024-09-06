using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TrocarFerramentaViaBotaoUI : MonoBehaviour
{
    [SerializeField] private FerramentaSO tipoFerramenta;

    private Button botao;
    private Image spriteFerramenta;
    private TextMeshProUGUI textMeshContadorTempo;

    private Boolean temContagemRegressiva = false;
    private int contadorTempo = 0;

    public Action<FerramentaSO> aoPressionarBotao;

    public void Start()
    {
        temContagemRegressiva = tipoFerramenta.ContagemRegressivaParaReuso > 0;
        
        botao = GetComponent<Button>();
        botao.onClick.AddListener(realizarMudancaFerramenta);

        textMeshContadorTempo = GetComponentInChildren<TextMeshProUGUI>();
        textMeshContadorTempo.enabled = false;
        
        spriteFerramenta = GetComponentInChildren<Image>();
        spriteFerramenta.sprite = tipoFerramenta.SpriteFerramenta;
    }

    public void realizarMudancaFerramenta()
    {
        aoPressionarBotao?.Invoke(tipoFerramenta);
    }

    public void aoUtilizarFerramenta(ResumoInteracaoBlocoFerramenta resumo)
    {
        if (temContagemRegressiva)
        {
            if (resumo.FerramentaUsada.Equals(tipoFerramenta))
                iniciarContagemRegressivaFerramenta();
            else
                diminuirContagemRegressiva(resumo.FerramentaUsada);

        }
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
}
