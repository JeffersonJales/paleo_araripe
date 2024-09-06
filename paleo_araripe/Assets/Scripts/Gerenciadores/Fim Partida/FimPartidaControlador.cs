using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FimPartidaControlador : MonoBehaviour
{
    [Range(10, 100)]
    [SerializeField] private int acoes = 10;
    [SerializeField] private int tempoEmSegundos = 30;
    [SerializeField] private Boolean porTempo = true;

    [SerializeField] private Slider uiSliderTempo;
    [SerializeField] private UtilizarFerramenta controladorFerramentas;

    private GameObject objetosUi;
    private FimPartidaAbstrato fimPartida;

    public Slider UiSliderTempo => uiSliderTempo;
    public UtilizarFerramenta ControladorFerramentas => controladorFerramentas;


    public void OnValidate()
    {
        tempoEmSegundos = Mathf.Clamp(tempoEmSegundos, 30, int.MaxValue); // or int.MaxValue, if you need to use an int but can't use uint.
    }

    void Start()
    {
        if (porTempo)
            configurarJogoPorTempo();
        else
            configurarJogoPorAcao();
    }

    public void configurarJogoPorTempo() 
    { 
        fimPartida = gameObject.AddComponent<FimPartidaPorTempo>();
        fimPartida.iniciar(tempoEmSegundos * 60, this);
    }
    public void configurarJogoPorAcao()
    {
        fimPartida = gameObject.AddComponent<FimPartidaPorAcao>();
        fimPartida.iniciar(acoes, this);
    }

    public void acabouTempo()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
