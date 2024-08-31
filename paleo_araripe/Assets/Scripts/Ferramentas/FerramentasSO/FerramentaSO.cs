using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FerramentasEscavacao", order = 1)]
public class FerramentaSO : ScriptableObject
{   

    [Tooltip("Tamanho da area que a ferramenta ira limpar")]
    [SerializeField] protected Vector3 areaEfeito = new Vector3(1, 1, 1);

    [Tooltip("Quanto de energia sera necessaria para utilizar a ferramenta")]
    [Range(0, 10)] [SerializeField] protected int energiaParaUsar = 0;                   

    [Tooltip("Apos usar a ferramenta, quanto tempo sera consumido")]
    [Range(1, 10)] [SerializeField] protected int tempoGastoAposUso = 1;                

    public Vector3 AreaEfeito => areaEfeito;
    public int EnergiaParaUsar => energiaParaUsar;
    public int TempoGastoAposUso => energiaParaUsar;
}
