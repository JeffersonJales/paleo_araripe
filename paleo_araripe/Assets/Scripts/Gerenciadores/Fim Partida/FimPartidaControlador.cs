using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FimPartidaControlador : MonoBehaviour
{
    [Range(10, 100)]
    [SerializeField] private int quantidadeAcoesParaAcabar = 10;
    private int quantidadeAcoesInicial = 0;

    [Range(1, 100)]
    [SerializeField] private int quantidadeAcoesGanhasPorAmbar = 0;
    
    [SerializeField] private int quantidadeFossils = 0;
    [SerializeField] private AtualizarValorSlider uiSliderTempo;
    public AtualizarValorSlider UiSliderTempo => uiSliderTempo;


    private FimPartidaAbstrato fimPartida;
    private UsarFerramentas controladorFerramentas;


    // Ocultei variáveis de tempo
    private int tempoEmSegundos = 30;
    private Boolean porTempo = false;
    
    public void OnValidate()
    {
        tempoEmSegundos = Mathf.Clamp(tempoEmSegundos, 30, int.MaxValue); 
    }

    void Start()
    {
        controladorFerramentas = GetComponent<UsarFerramentas>();
        
        if (porTempo)
            configurarJogoPorTempo();
        else
            configurarJogoPorAcao();

        BlocoGenerico[] blocosGenericosNaCena = FindObjectsOfType<BlocoGenerico>();
        foreach(var item in blocosGenericosNaCena)
        {
            if(item.BlocoSO.Tipo == NaturezaBlocoFerramenta.TipoBloco.FOSSIL)
                quantidadeFossils++;
        }

        uiSliderTempo.atualizarValorSlider(1f);
    }

    public void configurarJogoPorTempo() 
    { 
        fimPartida = gameObject.AddComponent<FimPartidaPorTempo>();
        fimPartida.iniciar(tempoEmSegundos * 60, this);

        controladorFerramentas.EventoResumoInteracao += verificarFimJogoPorFossilColetado;
    }
    public void configurarJogoPorAcao()
    {
        quantidadeAcoesInicial = quantidadeAcoesParaAcabar;
        controladorFerramentas.EventoResumoInteracao += verificarFimJogoPorFossilColetado;
        controladorFerramentas.EventoResumoInteracao += contabilizarAcaoAposInteragirComBloco;
    }

    public void verificarFimJogoPorFossilColetado(ResumoInteracaoBlocoFerramenta resumo) {
        quantidadeFossils -= resumo.QuantidadeFossilColetado;
        quantidadeFossils -= resumo.QuantidadeFossilDestruido;
        if (quantidadeFossils <= 0)
            acabouTempo();
    }

    public void contabilizarAcaoAposInteragirComBloco(ResumoInteracaoBlocoFerramenta resumo)
    {
        quantidadeAcoesParaAcabar -= resumo.FerramentaUsada.TempoGastoAposUso;
        quantidadeAcoesParaAcabar = Math.Min(quantidadeAcoesParaAcabar + quantidadeAcoesGanhasPorAmbar * resumo.QuantidadeAmbarColetado, quantidadeAcoesInicial);
        uiSliderTempo.atualizarValorSlider(quantidadeAcoesParaAcabar, quantidadeAcoesInicial);

        if (quantidadeAcoesParaAcabar <= 0)
            acabouTempo();
    }

    public void acabouTempo()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
