using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumoInteracaoBlocoFerramenta {
    private FerramentaSO ferramentaUsada = null;
    private Boolean aoMenosUmBlocoDestruido = false;

    private List<Boolean> blocosDestruidos = new List<bool>();
    private List<BlocosSO> blocosAfetados = new List<BlocosSO>();

    public FerramentaSO FerramentaUsada { get => ferramentaUsada; set => ferramentaUsada = value; }
    public List<BlocosSO> BlocosAfetados { get => blocosAfetados; set => blocosAfetados = value; }
    public List<bool> BlocosDestruidos { get => blocosDestruidos; set => blocosDestruidos = value; }
}
