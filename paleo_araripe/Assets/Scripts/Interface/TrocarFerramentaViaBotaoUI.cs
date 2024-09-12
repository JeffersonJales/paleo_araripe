using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor.SceneManagement;

public class TrocarFerramentaViaBotaoUI : MonoBehaviour
{
    [SerializeField] private FerramentaSO tipoFerramenta;
    [SerializeField] private TextMeshProUGUI textMeshContadorTempo;
    [SerializeField] private TextMeshProUGUI textMeshContadorJogadas;
    [SerializeField] private QuadroNegroSO quadroNegroInformacaoes;
    [SerializeField] private Boolean esperandoInspiracao = false;


    private Button botao;
    private Image spriteFerramenta;

    private Boolean emContragemRegressiva = false;

    private int contadorTempo = 0;
    private int inspiracaoNecessariaUso = 0;
    private int quantidadeUsosRestantes = 0;
    private UsarFerramentas usarFerramenta;
    public Action<FerramentaSO> aoPressionarBotao;

    public void Start()
    {
        Boolean temContagemRegressiva = tipoFerramenta.ContagemRegressivaParaReuso > 0;
        Boolean necessitaInspiracao = tipoFerramenta.Inspiracao < 0;
        Boolean temQuantidadeLimite = tipoFerramenta.QuantidadeLimiteDeUsos > 0;

        botao = GetComponent<Button>();
        botao.onClick.AddListener(realizarMudancaFerramenta);

        textMeshContadorTempo.enabled = false;
        textMeshContadorJogadas.enabled = false;
        
        spriteFerramenta = GetComponentInChildren<Image>();
        spriteFerramenta.sprite = tipoFerramenta.SpriteFerramenta;

        usarFerramenta = FindObjectOfType<UsarFerramentas>();
        if (usarFerramenta != null)
        {
            aoPressionarBotao += usarFerramenta.trocarFerramenta;

            if (necessitaInspiracao)
            {
                inspiracaoNecessariaUso = Math.Abs(tipoFerramenta.Inspiracao);
                usarFerramenta.EventoResumoInteracao += aoAlterarInspiracao;
                esperandoInspiracao = true;
                tentarDesabilitarBotao();
            }

            if (temContagemRegressiva)
                usarFerramenta.EventoResumoInteracao += aoUtilizarFerramenta;

            if (temQuantidadeLimite)
            {
                usarFerramenta.EventoResumoInteracao += gastarQuantidadeLimite;
                quantidadeUsosRestantes = tipoFerramenta.QuantidadeLimiteDeUsos;
                textMeshContadorJogadas.enabled = true;
                textMeshContadorJogadas.text = quantidadeUsosRestantes.ToString();
            }
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
        tentarDesabilitarBotao();
        aoPressionarBotao?.Invoke(null);

    }

    private void finalizarContagemRegressiva()
    {
        contadorTempo = 0;
        textMeshContadorTempo.enabled = false;
        tentarHabilitarBotao();
    }

    private void diminuirContagemRegressiva(FerramentaSO ferramenta)
    {
        if (contadorTempo > 0)
        {
            if (--contadorTempo <= 0)
                finalizarContagemRegressiva();

            textMeshContadorTempo.text = contadorTempo.ToString();
        }
    }

    private void aoAlterarInspiracao(ResumoInteracaoBlocoFerramenta resumo)
    {
        if (esperandoInspiracao && resumo.ganhouInspiraca() && quadroNegroInformacaoes.GetIntValue(QuadroNegroInfoJogoChaves.INSPIRACAO_ATUAL) >= inspiracaoNecessariaUso) {
            esperandoInspiracao = false;
            tentarHabilitarBotao();
        }
        else if(!esperandoInspiracao && resumo.gastouInspiaracao() && quadroNegroInformacaoes.GetIntValue(QuadroNegroInfoJogoChaves.INSPIRACAO_ATUAL) < inspiracaoNecessariaUso)
        {
            esperandoInspiracao = true;
            tentarDesabilitarBotao();
        }
    }

  

    private void gastarQuantidadeLimite(ResumoInteracaoBlocoFerramenta resumo)
    {
        if (resumo.FerramentaUsada.Equals(tipoFerramenta))
        {
            if(--quantidadeUsosRestantes <= 0)
            {
                tentarDesabilitarBotao();
                aoPressionarBotao?.Invoke(null);
                usarFerramenta.EventoResumoInteracao -= gastarQuantidadeLimite;
            }

            textMeshContadorJogadas.text = quantidadeUsosRestantes.ToString();
        }
    }

    [SerializeField] private int desabilitadoQtd = 0;

    private void tentarDesabilitarBotao() {
        if (desabilitadoQtd++ == 0)
        {
            botao.interactable = false;
            textMeshContadorJogadas.enabled = false;
            spriteFerramenta.color = new Color(1, 1, 1, 0.5f);
        }
    }

    private void tentarHabilitarBotao() {
        if (--desabilitadoQtd == 0)
        {
            botao.interactable = true;
            textMeshContadorJogadas.enabled = true;
            spriteFerramenta.color = new Color(1, 1, 1, 1);
        }
    }

}
