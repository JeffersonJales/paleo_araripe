using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TrocarFerramentaViaBotaoUI : MonoBehaviour
{
    [SerializeField] private Image spriteFerramenta;
    [SerializeField] private FerramentaSO tipoFerramenta;
    [SerializeField] private TextMeshProUGUI textMeshContadorTempo;

    private int contadorTempo = 0;
    private Boolean selecionado = false;

    public Action<FerramentaSO> aoPressionarBotao;

    public void Start()
    {
        spriteFerramenta.sprite = tipoFerramenta.SpriteFerramenta;
        GetComponent<Button>().onClick.AddListener(realizarMudancaFerramenta);
    }

    public void realizarMudancaFerramenta()
    {
        aoPressionarBotao?.Invoke(tipoFerramenta);
    }

    public void aoUtilizarFerramenta(FerramentaSO ferramenta)
    {
        
    }
}
