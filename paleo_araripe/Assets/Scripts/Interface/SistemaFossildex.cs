using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PaleoAraripe
{
    public class SistemaFossildex : MonoBehaviour
    {
        [SerializeField] private TMP_Text titulo;
        [SerializeField] private TMP_Text descricao;
        [SerializeField] private Image imagem;
        [SerializeField] private Image imagemBloqueado;

        [SerializeField] private List<FossildexItemSO> itens;
        [SerializeField] private List<GameObject> botoes;

        public void SetFossilUI(int itemID)
        {
            titulo.SetText(itens[itemID].Titulo);
            descricao.SetText(itens[itemID].Descricao);
            imagem.sprite = itens[itemID].Imagem;
        }
    }
}
