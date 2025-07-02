using TMPro;
using UnityEngine;

namespace PaleoAraripe
{
    /// <summary>
    /// Classe responsável por exibir o popup de informações detalhadas de um bloco quando a lupa é usada.
    /// </summary>
    public class UiLupa : MonoBehaviour
    {
        [SerializeField] private GameObject containerUi;
        [SerializeField] private TextMeshProUGUI titulo;
        [SerializeField] private TextMeshProUGUI descricao;

        public void Start()
        {
            DesligarPopup();
        }


        public void LigarPopup(BlocoGenerico bloco)
        {
            titulo.SetText(bloco.Nome);
            descricao.SetText(bloco.Descricao);
     
            containerUi.SetActive(true);
        }

        public void DesligarPopup()
        {
            containerUi.SetActive(false);
        }
    }
}
