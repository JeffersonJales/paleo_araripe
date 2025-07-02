using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaleoAraripe {
    public class MenuPrincipal : MonoBehaviour
    {
        [SerializeField] private GameObject telaPrincipal;
        [SerializeField] private GameObject telaSobre;
        [SerializeField] private GameObject telaTutorial;
        [SerializeField] private int menuFrameRate = 60;
        [SerializeField] private Animator animacaoCarregamento;

        private void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = menuFrameRate;
        }
        public void Jogar(int levelSelecionado)
        {
            GerenciadorDados.Instance.levelSelecionado = levelSelecionado;
            GerenciadorDados.Instance.jogoFinalizado = false;
            animacaoCarregamento.SetTrigger("carregar");
        }
        public void AbrirSobre()
        {
            telaPrincipal.SetActive(false);
            telaSobre.SetActive(true);
        }
        public void AbrirTutorial()
        {
            telaPrincipal.SetActive(false);
            telaTutorial.SetActive(true);
        }
        public void FecharSobre()
        {
            telaSobre.SetActive(false);
            telaPrincipal.SetActive(true);
        }
        public void FecharTutorial()
        {
            telaTutorial.SetActive(false);
            telaPrincipal.SetActive(true);
        }
    }
}
