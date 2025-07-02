using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaleoAraripe {
    /// <summary>
    /// Classe responsável por carregar uma nova cena no jogo a partir de um índice definido no Inspector.
    /// </summary>
    public class CarregarCena : MonoBehaviour
    {
        [SerializeField] private int indexCena;
        public void CarregarNovaCena()
        {
            SceneManager.LoadScene(indexCena, LoadSceneMode.Single);
        }
    }
}
