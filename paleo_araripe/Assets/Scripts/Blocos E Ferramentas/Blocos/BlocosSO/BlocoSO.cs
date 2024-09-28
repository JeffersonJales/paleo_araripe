using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BlocoSO", menuName = "ScriptableObjects/Blocos", order = 2)]

public class BlocoSO : ScriptableObject
{
    [Range(1, 3)] 
    [SerializeField] private int vida = 1;
    [SerializeField] private Boolean sofreDanoQuandoCuboCaiNele = false;

    [SerializeField] private NaturezaBlocoFerramenta.NivelDureza dureza = NaturezaBlocoFerramenta.NivelDureza.TERRA;
    [SerializeField] private NaturezaBlocoFerramenta.TipoBloco tipo = NaturezaBlocoFerramenta.TipoBloco.NORMAL;
    [SerializeField] private NaturezaBlocoFerramenta.IdentificadorBloco identificador = NaturezaBlocoFerramenta.IdentificadorBloco.AMBAR;
    
    [SerializeField] private GameObject feedbackAoDestruir;
    [SerializeField] private GameObject feedbackAoColetar;
    [SerializeField] private Material corMaterialDestacado = null;
    [SerializeField] private Material corMaterialNaoDestacado = null;

    public int Vida => vida;

    public NaturezaBlocoFerramenta.NivelDureza TipoDureza => dureza;
    public NaturezaBlocoFerramenta.TipoBloco Tipo => tipo;

    public GameObject FeedbackAoColetar => feedbackAoColetar;
    public GameObject FeedbackAoDestruir => feedbackAoDestruir;

    public Material CorMaterialDestacado => corMaterialDestacado;
    public Material CorMaterialNaoDestacado => corMaterialNaoDestacado;
    
    public bool SofreDanoQuandoCuboCaiNele => sofreDanoQuandoCuboCaiNele;

    public NaturezaBlocoFerramenta.IdentificadorBloco Identificador => identificador;
}

