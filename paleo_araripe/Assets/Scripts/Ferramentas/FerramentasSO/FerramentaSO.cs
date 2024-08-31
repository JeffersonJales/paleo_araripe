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

    [Tooltip("Quanto de dano ele irá causar no bloco")]
    [Range(1, 10)][SerializeField] private int dano = 1;



    public Vector3 AreaEfeito => areaEfeito;
    public int EnergiaParaUsar => energiaParaUsar;
    public int TempoGastoAposUso => tempoGastoAposUso;
    public int Dano => dano;
}
