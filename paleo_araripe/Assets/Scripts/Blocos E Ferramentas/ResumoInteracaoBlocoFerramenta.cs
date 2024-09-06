using System;
using System.Collections.Generic;

public class ResumoInteracaoBlocoFerramenta {
    private FerramentaSO ferramentaUsada = null;
    private List<Boolean> blocosDestruidos = new List<bool>();
    private List<BlocosSO> blocosAfetados = new List<BlocosSO>();

    public FerramentaSO FerramentaUsada { get => ferramentaUsada; set => ferramentaUsada = value; }
    public List<BlocosSO> BlocosAfetados { get => blocosAfetados; set => blocosAfetados = value; }
    public List<bool> BlocosDestruidos { get => blocosDestruidos; set => blocosDestruidos = value; }
}
