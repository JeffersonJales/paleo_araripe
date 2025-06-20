using TMPro;
using UnityEngine;

namespace PaleoAraripe
{
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
            titulo.SetText(bloco.NomeBloco);
            descricao.SetText(bloco.DescricaoBloco);
     
            containerUi.SetActive(true);
        }

        public void DesligarPopup()
        {
            containerUi.SetActive(false);
        }
    }
}
