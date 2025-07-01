using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaleoAraripe {
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
