using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PaleoAraripe
{
    /// <summary>
    /// Fossildex: ícones bloqueados ficam com tamanho uniforme; ao desbloquear, voltam ao tamanho original.
    /// </summary>
    public class SistemaFossildex : MonoBehaviour
    {
        [Header("Painel de Detalhes")]
        [SerializeField] private TMP_Text titulo;
        [SerializeField] private TMP_Text descricao;
        [SerializeField] private Image imagem;
        [SerializeField] private Sprite imagemBloqueado;

        [Header("Sprites / Botões")]
        [SerializeField] private Sprite imagemBotaoInterrogacao;

        [Header("Dados")]
        [SerializeField] private List<FossildexItemSO> itens;

        [Header("UI - Botões (raiz do botão)")]
        [SerializeField] private List<GameObject> botoes;

        [Header("Layout - Ícones Bloqueados")]
        [Tooltip("Tamanho que TODOS os ícones terão quando estiverem bloqueados.")]
        [SerializeField] private Vector2 tamanhoIconeBloqueado = new Vector2(180f, 180f);
        [Tooltip("Se usa LayoutGroup, marcar para forçar via LayoutElement (preferredWidth/Height).")]
        [SerializeField] private bool aplicarViaLayoutElement = true;

        private readonly List<Button> btnComponentes = new List<Button>();
        private readonly List<Image> imagensDosBotoes = new List<Image>();

        // backups para restaurar o tamanho original ao desbloquear
        private readonly List<RectTransform> rtIcones = new List<RectTransform>();
        private readonly List<Vector2> tamanhosOriginais = new List<Vector2>();
        private readonly List<LayoutElement> layoutIcones = new List<LayoutElement>();
        private readonly List<Vector2> preferredOriginais = new List<Vector2>(); // (-1,-1) = sem preferred definido

        private bool[] niveisCompletos;
        private bool[] fosseisEncontrados;

        private void Awake()
        {
            int total = Mathf.Min(itens.Count, botoes.Count);

            for (int i = 0; i < total; i++)
            {
                var btn = botoes[i].GetComponent<Button>();
                btnComponentes.Add(btn);

                // pega o Image do ícone do botão de forma robusta
                var img = EncontrarImagemDoBotao(botoes[i]);
                imagensDosBotoes.Add(img);

                // guarda referências e tamanhos originais
                RectTransform rt = img ? img.rectTransform : null;
                rtIcones.Add(rt);
                tamanhosOriginais.Add(rt ? rt.sizeDelta : Vector2.zero);

                var le = img ? img.GetComponent<LayoutElement>() : null;
                layoutIcones.Add(le);
                preferredOriginais.Add(le ? new Vector2(le.preferredWidth, le.preferredHeight) : new Vector2(-1f, -1f));

                int valor = i; // captura do índice
                btn.onClick.AddListener(() => SetFossilUI(valor));
            }
        }

        private Image EncontrarImagemDoBotao(GameObject raiz)
        {
            var imagemDoProprioBotao = raiz.GetComponent<Image>();
            var images = raiz.GetComponentsInChildren<Image>(true);
            foreach (var img in images)
            {
                if (imagemDoProprioBotao != null && img == imagemDoProprioBotao) continue;
                return img; // primeiro filho com Image
            }
            if (raiz.transform.childCount > 0)
                return raiz.transform.GetChild(0).GetComponent<Image>();
            return null;
        }

        private void OnEnable()
        {
            ConfigurarBotoes();
            AutoSelecionarPrimeiroDesbloqueadoOuPlaceholder();
        }

        private void ConfigurarBotoes()
        {
            SalvarCarregar.Instance.Carregar(out niveisCompletos, out fosseisEncontrados);

            for (int i = 0; i < itens.Count; i++)
            {
                bool desbloqueado = i < (fosseisEncontrados?.Length ?? 0) && fosseisEncontrados[i];

                if (i < btnComponentes.Count)
                    btnComponentes[i].interactable = desbloqueado;

                var img = i < imagensDosBotoes.Count ? imagensDosBotoes[i] : null;
                if (img == null) continue;

                if (desbloqueado)
                {
                    // ÍCONE DO BOTÃO desbloqueado: usa IconeBotao (fallback para ImagemGrande) e RESTAURA tamanho original
                    img.sprite = itens[i].IconeBotao != null ? itens[i].IconeBotao : itens[i].ImagemGrande;
                    img.enabled = true;
                    img.preserveAspect = true;
                    RestaurarTamanhoOriginal(i);
                }
                else
                {
                    // BLOQUEADO: interrogação + tamanho uniforme
                    img.sprite = imagemBotaoInterrogacao;
                    img.enabled = true;
                    img.preserveAspect = true;
                    AplicarTamanhoBloqueado(i);
                }
            }
        }

        private void AplicarTamanhoBloqueado(int index)
        {
            var rt = rtIcones[index];
            if (rt != null) rt.sizeDelta = tamanhoIconeBloqueado;

            if (aplicarViaLayoutElement && layoutIcones[index] != null)
            {
                var le = layoutIcones[index];
                le.preferredWidth = tamanhoIconeBloqueado.x;
                le.preferredHeight = tamanhoIconeBloqueado.y;
            }
        }

        private void RestaurarTamanhoOriginal(int index)
        {
            var rt = rtIcones[index];
            if (rt != null) rt.sizeDelta = tamanhosOriginais[index];

            if (aplicarViaLayoutElement && layoutIcones[index] != null)
            {
                var le = layoutIcones[index];
                var pref = preferredOriginais[index];
                le.preferredWidth = pref.x;
                le.preferredHeight = pref.y;
            }
        }

        private void AutoSelecionarPrimeiroDesbloqueadoOuPlaceholder()
        {
            int primeiro = -1;
            if (fosseisEncontrados != null)
            {
                for (int i = 0; i < itens.Count && i < fosseisEncontrados.Length; i++)
                {
                    if (fosseisEncontrados[i]) { primeiro = i; break; }
                }
            }
            if (primeiro >= 0) SetFossilUI(primeiro);
            else MostrarPlaceholder();
        }

        private void MostrarPlaceholder()
        {
            titulo.SetText("???");
            descricao.SetText("???");
            imagem.sprite = imagemBloqueado;
            imagem.enabled = true;
            imagem.preserveAspect = true;
        }

        public void SetFossilUI(int itemID)
        {
            if (itemID < 0 || itemID >= itens.Count || itens[itemID] == null)
            {
                MostrarPlaceholder();
                return;
            }

            var item = itens[itemID];
            titulo.SetText(item.Titulo);
            descricao.SetText(item.Descricao);

            imagem.sprite = item.ImagemGrande != null ? item.ImagemGrande : imagemBloqueado;
            imagem.enabled = true;
            imagem.preserveAspect = true;
        }
    }
}
