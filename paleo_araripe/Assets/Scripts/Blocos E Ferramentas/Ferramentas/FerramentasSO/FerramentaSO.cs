using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FerramentasEscavacao", order = 1)]
public class FerramentaSO : ScriptableObject
{   


    [Tooltip("Quanto de dano ele irá causar no bloco")]
    [Range(1, 10)][SerializeField] private int dano = 1; 
    
    [Tooltip("Quanto de inspiração a ferramenta gasta / dá ao jogador")]
    [Range(-10, 10)][SerializeField] private int inspiracao = 1;

    [Tooltip("Apos usar a ferramenta, quanto tempo sera consumido")]
    [Range(1, 50)] [SerializeField] private int tempoGastoAposUso = 1;

    [Tooltip("Após usar, quanto tempo fica sem ser usável")]
    [Range(0, 10)][SerializeField] private int contagemRegressivaParaReuso = 0;

    [Tooltip("Quantas vezes essa ferramenta pode ser usada no jogo")]
    [Range(0, 25)][SerializeField] private int quantidadeLimiteDeUsos = 0;

    [SerializeField] private List<BlocoSO> daDanoEm;
    [SerializeField] private List<BlocoSO> consegueColetar;
    [SerializeField] private NaturezaBlocoFerramenta.TipoColisaoFerramenta tipoColisao;


    [Tooltip("Sprite que representa a ferramenta")]
    [SerializeField] private Sprite sprite;

    [Header("Obsoleto")]
    [Tooltip("Tamanho da area que a ferramenta ira limpar")]
    [SerializeField] private Vector3 areaEfeito = new Vector3(1, 1, 1);
    [SerializeField] private NaturezaBlocoFerramenta.NivelDureza quebraAte = NaturezaBlocoFerramenta.NivelDureza.TERRA;
    [SerializeField] private NaturezaBlocoFerramenta.TipoInteracao interacao = NaturezaBlocoFerramenta.TipoInteracao.DELICADO;

    // Getters
    public int TempoGastoAposUso => tempoGastoAposUso;
    public int Dano => dano;
    public int Inspiracao => inspiracao;
    public Sprite SpriteFerramenta => sprite;
    public int ContagemRegressivaParaReuso => contagemRegressivaParaReuso;
    public int QuantidadeLimiteDeUsos => quantidadeLimiteDeUsos;
    public List<BlocoSO> DaDanoEm => daDanoEm;
    public List<BlocoSO> ConsegueColetar => consegueColetar;

    public NaturezaBlocoFerramenta.TipoColisaoFerramenta TipoColisao => tipoColisao;

    public Vector3 AreaEfeito => areaEfeito;
    public NaturezaBlocoFerramenta.NivelDureza QuebraQueDureza => quebraAte;
    public NaturezaBlocoFerramenta.TipoInteracao Interacao => interacao;

}
