using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaleoAraripe {
    public class MenuPrincipal : MonoBehaviour
    {
        [SerializeField] private GameObject telaPrincipal;
        [SerializeField] private GameObject telaSobre;
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
        public void FecharSobre()
        {
            telaSobre.SetActive(false);
            telaPrincipal.SetActive(true);
        }
    }
}
