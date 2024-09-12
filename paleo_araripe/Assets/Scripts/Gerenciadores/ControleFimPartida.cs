using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleFimPartida : MonoBehaviour
{
    [Range(10, 100)]
    [SerializeField] private int acoesParaFimJogo = 10;

    [Range(1, 100)]
    [SerializeField] private int acoesGanhasPorAmbar = 0;
    [SerializeField] private int quantidadeFossils = 0;

    [SerializeField] private AtualizarValorSlider uiSliderTempo; // Slider para mostrar quantidade de tempo
    
    private UsarFerramentas controladorFerramentas;
    private int quantidadeAcoesInicial = 0;

    /// Getters / Setters
    public AtualizarValorSlider UiSliderTempo => uiSliderTempo;


    void Start()
    {
        controladorFerramentas = GetComponent<UsarFerramentas>();

        quantidadeAcoesInicial = acoesParaFimJogo;
        controladorFerramentas.EventoAposRealizarUsoFerramenta += verificarFimFosseis;
        controladorFerramentas.EventoAposRealizarUsoFerramenta += verificarFimSemAcoes;

        /// Catar quantidade de fóssies para acabar o jogo
        BlocoGenerico[] blocosGenericosNaCena = FindObjectsOfType<BlocoGenerico>();
        foreach(var item in blocosGenericosNaCena)
        {
            if(item.BlocoSO.Tipo == NaturezaBlocoFerramenta.TipoBloco.FOSSIL)
                quantidadeFossils++;
        }

        uiSliderTempo.atualizarValorSlider(1f);
    }

    public void verificarFimFosseis(ResumoInteracaoBlocoFerramenta resumo) {
        quantidadeFossils -= resumo.QuantidadeFossilColetado;
        quantidadeFossils -= resumo.QuantidadeFossilDestruido;
        if (quantidadeFossils <= 0)
            finalizarPartida();
    }

    public void verificarFimSemAcoes(ResumoInteracaoBlocoFerramenta resumo)
    {
        acoesParaFimJogo -= resumo.FerramentaUsada.TempoGastoAposUso;
        acoesParaFimJogo = Math.Min(acoesParaFimJogo + acoesGanhasPorAmbar * resumo.QuantidadeAmbarColetado, quantidadeAcoesInicial);
        uiSliderTempo.atualizarValorSlider(acoesParaFimJogo, quantidadeAcoesInicial);

        if (acoesParaFimJogo <= 0)
            finalizarPartida();
    }

    public void finalizarPartida()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

}
