using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private GameObject telaPrincipal;
    [SerializeField] private GameObject telaSobre;
    [SerializeField] private int menuFrameRate = 30;
    [SerializeField] private Animator animacaoCarregamento;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = menuFrameRate;
    }
    public void Jogar()
    {
        animacaoCarregamento.SetTrigger("carregar");
    }
    public void AbrirSobre()
    {
        telaPrincipal.SetActive(false);
        telaSobre.SetActive(true);
    }
    public void FecharSobre()
    {
        telaSobre.SetActive(false);
        telaPrincipal.SetActive(true);
    }
}
