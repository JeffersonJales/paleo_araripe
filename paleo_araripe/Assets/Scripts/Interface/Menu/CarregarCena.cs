using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaleoAraripe {
    public class CarregarCena : MonoBehaviour
    {
        [SerializeField] private int indexCena;
        public void CarregarNovaCena()
        {
            SceneManager.LoadScene(indexCena, LoadSceneMode.Single);
        }
    }
}
