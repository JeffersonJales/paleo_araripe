using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PaleoAraripe
{
    /// <summary>
    /// Classe responsável por gerenciar a interface e lógica da Fossildex, exibindo informações dos fósseis desbloqueados e bloqueados.
    /// </summary>
    public class SistemaFossildex : MonoBehaviour
    {
        [SerializeField] private TMP_Text titulo;
        [SerializeField] private TMP_Text descricao;
        [SerializeField] private Image imagem;
        [SerializeField] private Sprite imagemBloqueado;
        [SerializeField] private Sprite imagemBotaoInterrogacao;

        [SerializeField] private List<FossildexItemSO> itens;
        [SerializeField] private List<GameObject> botoes;
        private List<Button> btnComponentes = new List<Button>();
        private List<Image> imagemFundoBotao = new List<Image>();

        public void Awake()
        {   
            for(int i = 0; i < itens.Count; i++)
            {
                Button btn = botoes[i].GetComponent<Button>();

                int valor = i;
                btn.onClick.AddListener(() => SetFossilUI(valor));
                btnComponentes.Add(btn);

                imagemFundoBotao.Add(botoes[i].transform.GetChild(0).GetComponent<Image>());
            }
        }

        public void OnEnable()
        {
            ConfigurarBotoes();

            titulo.SetText("???");
            descricao.SetText("???");
            imagem.sprite = imagemBloqueado;
        }

        private void ConfigurarBotoes()
        {
            bool[] niveisCompletos, fosseisEncontrados;
            SalvarCarregar.Instance.Carregar(out niveisCompletos, out fosseisEncontrados);

            for (int i = 0; i < itens.Count; i++)
            {
                btnComponentes[i].interactable = fosseisEncontrados[i];
                imagemFundoBotao[i].sprite = fosseisEncontrados[i] ? itens[i].Imagem : imagemBotaoInterrogacao;
            }

        }

        public void SetFossilUI(int itemID)
        {
            titulo.SetText(itens[itemID].Titulo);
            descricao.SetText(itens[itemID].Descricao);
            imagem.sprite = itens[itemID].Imagem;
        }
    }
}
