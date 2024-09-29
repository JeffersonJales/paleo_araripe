using System;
using System.Collections.Generic;
using UnityEngine;

public class ResumoInteracaoBlocoFerramenta {
    private FerramentaSO ferramentaUsada = null;
    private List<NaturezaBlocoFerramenta.ResultadoInteracao> tipoInteracaoBloco = new List<NaturezaBlocoFerramenta.ResultadoInteracao>();
    private List<BlocoSO> blocosAfetados = new List<BlocoSO>();
    private List<GameObject> blocosDestruidos = new List<GameObject>();

    private int quantidadeFossilColetado = 0;
    private int quantidadeFossilDestruido = 0;
    private int quantidadeAmbarColetado = 0;
    private int quantidadeInspiracaoGanha = 0;
    private bool algumBlocoDestruidoOuColeado = false;

    private bool ferramentaCongelada = false;

    public FerramentaSO FerramentaUsada { get => ferramentaUsada; set => ferramentaUsada = value; }
    public List<BlocoSO> BlocosAfetados { get => blocosAfetados; set => blocosAfetados = value; }
    public List<NaturezaBlocoFerramenta.ResultadoInteracao> TipoInteracaoBloco { get => tipoInteracaoBloco; set => tipoInteracaoBloco = value; }
    public int QuantidadeFossilColetado { get => quantidadeFossilColetado; set => quantidadeFossilColetado = value; }
    public int QuantidadeFossilDestruido { get => quantidadeFossilDestruido; set => quantidadeFossilDestruido = value; }
    public int QuantidadeAmbarColetado { get => quantidadeAmbarColetado; set => quantidadeAmbarColetado = value; }
    public bool AlgumBlocoDestruidoOuColeado { get => algumBlocoDestruidoOuColeado; set => algumBlocoDestruidoOuColeado = value; }
    public int QuantidadeInspiracaoGanha { get => quantidadeInspiracaoGanha; set => quantidadeInspiracaoGanha = value; }
    public List<GameObject> BlocosDestruidos { get => blocosDestruidos; set => blocosDestruidos = value; }
    public bool FerramentaCongelada { get => ferramentaCongelada; set => ferramentaCongelada = value; }

    public Boolean gastouInspiaracao()
    {
        return ferramentaUsada.Inspiracao < 0;
    }

    public Boolean ganhouInspiraca()
    {
        return ferramentaUsada.Inspiracao > 0;
    }


}
