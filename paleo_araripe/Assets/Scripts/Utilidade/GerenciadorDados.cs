using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaleoAraripe {
    /// <summary>
    /// Classe singleton responsável por armazenar dados globais do jogo, como nível selecionado e estado de finalização.
    /// </summary>
    public class GerenciadorDados : Singleton<GerenciadorDados>
    {
        public int levelSelecionado;
        public bool jogoFinalizado;

        [SerializeField] private int indexCena;
        public void Start()
        {
            SceneManager.LoadScene(indexCena, LoadSceneMode.Single);
        }
    }
}
