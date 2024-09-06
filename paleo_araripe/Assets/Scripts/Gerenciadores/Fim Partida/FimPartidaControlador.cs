using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FimPartidaControlador : MonoBehaviour
{
    [Range(10, 100)]
    [SerializeField] private int acoes = 10;
    [SerializeField] private Slider uiSliderTempo;
    [SerializeField] private UsarFerramentas controladorFerramentas;

    private GameObject objetosUi;
    private FimPartidaAbstrato fimPartida;

    // Ocultei variáveis de tempo
    private int tempoEmSegundos = 30;
    private Boolean porTempo = false;



    public Slider UiSliderTempo => uiSliderTempo;
    public UsarFerramentas ControladorFerramentas => controladorFerramentas;


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
