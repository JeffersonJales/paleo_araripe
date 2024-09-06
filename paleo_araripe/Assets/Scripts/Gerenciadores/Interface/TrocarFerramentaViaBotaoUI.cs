using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TrocarFerramentaViaBotaoUI : MonoBehaviour
{
    [SerializeField] private Image spriteFerramenta;
    [SerializeField] private Button botao;
    [SerializeField] private TextMeshProUGUI textMeshContadorTempo;
    [SerializeField] private FerramentaSO tipoFerramenta;

    private int contadorTempo = 0;
    private Boolean ativado = false;

    public Button Botao => botao;

}
