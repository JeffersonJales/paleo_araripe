using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FerramentasEscavacao", order = 1)]
public class FerramentaSO : ScriptableObject
{   

    [Tooltip("Tamanho da area que a ferramenta ira limpar")]
    [SerializeField] private Vector3 areaEfeito = new Vector3(1, 1, 1);

    [Tooltip("Quanto de energia sera necessaria para utilizar a ferramenta")]
    [Range(0, 10)] [SerializeField] private int energiaParaUsar = 0;                   

    [Tooltip("Apos usar a ferramenta, quanto tempo sera consumido")]
    [Range(1, 10)] [SerializeField] private int tempoGastoAposUso = 1;

    [Tooltip("Quanto de dano ele ir� causar no bloco")]
    [Range(1, 10)][SerializeField] private int dano = 1;

    [Tooltip("Ap�s usar, quanto tempo fica sem ser us�vel")]
    [Range(0, 10)][SerializeField] private int contagemRegressivaParaReuso = 0;

    [Tooltip("Essa ferramenta � usada para quebrar at� que tipo de bloco")]
    [SerializeField] private NaturezaBlocoFerramenta.NivelDureza quebraAte = NaturezaBlocoFerramenta.NivelDureza.TERRA;

    [Tooltip("Como se d� a intera��o entre bloco e ferramenta")]
    [SerializeField] private NaturezaBlocoFerramenta.TipoInteracao interacao = NaturezaBlocoFerramenta.TipoInteracao.DELICADO;


    [Tooltip("Sprite que representa a ferramenta")]
    [SerializeField] private Sprite sprite;



    // Getters
    public Vector3 AreaEfeito => areaEfeito;
    public int EnergiaParaUsar => energiaParaUsar;
    public int TempoGastoAposUso => tempoGastoAposUso;
    public int Dano => dano;
    public Sprite SpriteFerramenta => sprite;
    public int ContagemRegressivaParaReuso => contagemRegressivaParaReuso;
    public NaturezaBlocoFerramenta.NivelDureza QuebraQueDureza => quebraAte;
    public NaturezaBlocoFerramenta.TipoInteracao Interacao => interacao;
}
