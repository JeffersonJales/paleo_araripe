using UnityEngine;

[CreateAssetMenu(fileName = "BlocoSO", menuName = "ScriptableObjects/Blocos", order = 2)]

public class BlocoSO : ScriptableObject
{
    [Range(1, 3)] 
    [SerializeField] private int vida = 1;
    [SerializeField] private NaturezaBlocoFerramenta.NivelDureza dureza = NaturezaBlocoFerramenta.NivelDureza.TERRA;
    [SerializeField] private NaturezaBlocoFerramenta.TipoBloco tipo = NaturezaBlocoFerramenta.TipoBloco.NORMAL;
    [SerializeField] private GameObject modelo;
    [SerializeField] private GameObject feedbackAoDestruir;
    [SerializeField] private Material corMaterialDestacado = null;
    [SerializeField] private Material corMaterialNaoDestacado = null;

    public int Vida => vida;
    public GameObject Modelo => modelo;
    public NaturezaBlocoFerramenta.NivelDureza TipoDureza => dureza;
    public NaturezaBlocoFerramenta.TipoBloco Tipo => tipo;
    public Material CorMaterialDestacado => corMaterialDestacado; 
    public Material CorMaterialNaoDestacado => corMaterialNaoDestacado;
    public GameObject FeedbackAoDestruir => feedbackAoDestruir; 
}

