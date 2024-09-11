using System;
using System.Collections.Generic;

public class ResumoInteracaoBlocoFerramenta {
    private FerramentaSO ferramentaUsada = null;
    private List<NaturezaBlocoFerramenta.ResultadoInteracao> tipoInteracaoBloco = new List<NaturezaBlocoFerramenta.ResultadoInteracao>();
    private List<BlocoSO> blocosAfetados = new List<BlocoSO>();
    private int quantidadeFossilColetado = 0;
    private int quantidadeFossilDestruido = 0;
    private int quantidadeAmbarColetado = 0;


    public FerramentaSO FerramentaUsada { get => ferramentaUsada; set => ferramentaUsada = value; }
    public List<BlocoSO> BlocosAfetados { get => blocosAfetados; set => blocosAfetados = value; }
    public List<NaturezaBlocoFerramenta.ResultadoInteracao> TipoInteracaoBloco { get => tipoInteracaoBloco; set => tipoInteracaoBloco = value; }
    public int QuantidadeFossilColetado { get => quantidadeFossilColetado; set => quantidadeFossilColetado = value; }
    public int QuantidadeFossilDestruido { get => quantidadeFossilDestruido; set => quantidadeFossilDestruido = value; }
    public int QuantidadeAmbarColetado { get => quantidadeAmbarColetado; set => quantidadeAmbarColetado = value; }
}
