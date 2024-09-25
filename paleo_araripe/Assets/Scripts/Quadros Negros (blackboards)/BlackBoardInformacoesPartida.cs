using UnityEngine;

[CreateAssetMenu(fileName = "QuadroNegroPartida", menuName = "ScriptableObjects/Quadro Negro/Info Partida")]
public class BlackBoardInformacoesPartida : BlackBoardSO
{
    public readonly string INSPIRACAO_ATUAL = "inspiracao_atual";
    public readonly string INSPIRACAO_MAXIMA = "inspiracao_maxima";

    public void OnEnable()
    {
        SetValue(INSPIRACAO_ATUAL, 0);
        SetValue(INSPIRACAO_MAXIMA, 0);
    }
}
