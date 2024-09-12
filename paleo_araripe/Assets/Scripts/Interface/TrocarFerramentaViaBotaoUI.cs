using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TrocarFerramentaViaBotaoUI : MonoBehaviour
{
    [SerializeField] private FerramentaSO tipoFerramenta;
    [SerializeField] private TextMeshProUGUI textMeshContadorTempo;
    [SerializeField] private TextMeshProUGUI textMeshContadorJogadas;
    [SerializeField] private BlackBoardSO quadroNegroInformacaoes;
    [SerializeField] private Boolean esperandoInspiracao = false;

    [SerializeField] private int desabilitadoQtd = 0;

    private Button botao;
    private Image spriteFerramenta;

    private int contadorTempo = 0;
    private int inspiracaoNecessariaUso = 0;
    private int quantidadeUsosRestantes = 0;
    private UsarFerramentas usarFerramenta;

    public void Start()
    {
        textMeshContadorTempo.enabled = false;
        textMeshContadorJogadas.enabled = false;
        
        spriteFerramenta = GetComponentInChildren<Image>();
        spriteFerramenta.sprite = tipoFerramenta.SpriteFerramenta;

        usarFerramenta = FindObjectOfType<UsarFerramentas>();
        if (usarFerramenta != null)
        {
            configurarComportamentoBotao();

            Boolean temContagemRegressiva = tipoFerramenta.ContagemRegressivaParaReuso > 0;
            Boolean necessitaInspiracao = tipoFerramenta.Inspiracao < 0;
            Boolean temQuantidadeLimite = tipoFerramenta.QuantidadeLimiteDeUsos > 0;

            if (necessitaInspiracao)
                configurarInspiracao();

            if (temContagemRegressiva)
                configurarContagemRegressiva();

            if (temQuantidadeLimite)
                configurarQuantidadeUsoLimitado();
        }
    }

    #region Comportamento do botão
    public Action<FerramentaSO> eventoTentativaTrocaFerramenta;

    private void configurarComportamentoBotao()
    {
        botao = GetComponent<Button>();
        botao.onClick.AddListener(forcarTrocaFerramenta);
        eventoTentativaTrocaFerramenta += usarFerramenta.trocarFerramentaEquipada;
    }

    public void forcarTrocaFerramenta()
    {
        eventoTentativaTrocaFerramenta?.Invoke(tipoFerramenta);
    }

    private void tentarDesabilitarBotao()
    {
        if (desabilitadoQtd++ == 0)
        {
            botao.interactable = false;
            textMeshContadorJogadas.enabled = false;
            spriteFerramenta.color = new Color(1, 1, 1, 0.5f);
        }
    }

    private void tentarHabilitarBotao()
    {
        if (--desabilitadoQtd == 0)
        {
            botao.interactable = true;
            textMeshContadorJogadas.enabled = true;
            spriteFerramenta.color = new Color(1, 1, 1, 1);
        }
    }

    #endregion

    #region Inspiração
    private void configurarInspiracao()
    {
        inspiracaoNecessariaUso = Math.Abs(tipoFerramenta.Inspiracao);
        usarFerramenta.EventoAposRealizarUsoFerramenta += verificarUsoDeAcordoComInspiracao;
        esperandoInspiracao = true;
        tentarDesabilitarBotao();
    }

    private void verificarUsoDeAcordoComInspiracao(ResumoInteracaoBlocoFerramenta resumo)
    {
        if (esperandoInspiracao && resumo.ganhouInspiraca() && quadroNegroInformacaoes.GetIntValue(BBChaveTuplaInfomacoesPartida.INSPIRACAO_ATUAL) >= inspiracaoNecessariaUso)
        {
            esperandoInspiracao = false;
            tentarHabilitarBotao();
        }
        else if (!esperandoInspiracao && resumo.gastouInspiaracao() && quadroNegroInformacaoes.GetIntValue(BBChaveTuplaInfomacoesPartida.INSPIRACAO_ATUAL) < inspiracaoNecessariaUso)
        {
            esperandoInspiracao = true;
            tentarDesabilitarBotao();
        }
    }
    #endregion

    #region Contagem Regressiva para usar novamente
    private void configurarContagemRegressiva()
    {
        usarFerramenta.EventoAposRealizarUsoFerramenta += verificarContagemRegressivaAposUsoDeFerramenta;
    }

    public void verificarContagemRegressivaAposUsoDeFerramenta(ResumoInteracaoBlocoFerramenta resumo)
    {
        if (resumo.FerramentaUsada.Equals(tipoFerramenta))
            iniciarContagemRegressiva();
        else
            diminuirContagemRegressiva(resumo.FerramentaUsada);
    }

    private void iniciarContagemRegressiva()
    {
        contadorTempo = tipoFerramenta.ContagemRegressivaParaReuso;
        textMeshContadorTempo.enabled = true;
        textMeshContadorTempo.text = contadorTempo.ToString();
        tentarDesabilitarBotao();
        eventoTentativaTrocaFerramenta?.Invoke(null);
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

    private void finalizarContagemRegressiva()
    {
        contadorTempo = 0;
        textMeshContadorTempo.enabled = false;
        tentarHabilitarBotao();
    }
    #endregion

    #region Limite de usos da ferramenta
    private void configurarQuantidadeUsoLimitado()
    {
        usarFerramenta.EventoAposRealizarUsoFerramenta += diminuirQuantidadeDeUsos;
        quantidadeUsosRestantes = tipoFerramenta.QuantidadeLimiteDeUsos;
        textMeshContadorJogadas.enabled = true;
        textMeshContadorJogadas.text = quantidadeUsosRestantes.ToString();
    }

    private void diminuirQuantidadeDeUsos(ResumoInteracaoBlocoFerramenta resumo)
    {
        if (resumo.FerramentaUsada.Equals(tipoFerramenta))
        {
            if (--quantidadeUsosRestantes <= 0)
            {
                tentarDesabilitarBotao();
                eventoTentativaTrocaFerramenta?.Invoke(null);
                usarFerramenta.EventoAposRealizarUsoFerramenta -= diminuirQuantidadeDeUsos;
            }

            textMeshContadorJogadas.text = quantidadeUsosRestantes.ToString();
        }
    }

    #endregion

}
