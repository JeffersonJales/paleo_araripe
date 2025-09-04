using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // <- para TMP_Text

namespace PaleoAraripe
{
    /// <summary>
    /// Controla estado da partida, fim de jogo, fósseis e integração com UI.
    /// </summary>
    public class ControladorEstadoPartida : MonoBehaviour
    {
        [Range(10, 1000)]
        [SerializeField] private int acoesParaFimJogo = 100;

        [Range(1, 100)]
        [SerializeField] private int acoesGanhasPorAmbar = 0;

        [SerializeField] private int fosseisParaColetar = 0;
        private int fosseisColetados = 0;
        private int fosseisDanificados = 0; // conta quantos foram danificados

        [SerializeField] private AtualizarValorSlider uiSliderTempo; // Slider de tempo/ações
        [SerializeField] private Animator animacaoCarregamento;
        [SerializeField] private GameObject telaFinal;
        
        // --- UI Final: slots dos fósseis e mensagem/botão ---
        [Header("UI Final")]
        [SerializeField] private Image[] slotsFosseis;             // 3 imagens no painel final
        // Mantidos por compatibilidade, mas não usados quando seguimos o esquema da HUD:
        [SerializeField] private Sprite spriteFossilAtivo;         // (não usado aqui)
        [SerializeField] private Sprite spriteFossilApagado;       // (não usado aqui)
        [SerializeField] private TMP_Text mensagemFinal;           // texto "Muito bem" / "Tente novamente"
        [SerializeField] private GameObject botaoTentarNovamente;  // botão que aparece ao falhar

        [SerializeField] private UnityEngine.UI.Image imagemProfessora;
        [SerializeField] private Sprite professoraNenhum;   // 0 fósseis
        [SerializeField] private Sprite professoraParcial;  // 1 ou 2 fósseis
        [SerializeField] private Sprite professoraCompleto; // 3 fósseis

        // --- UI Durante a Partida (HUD) ---
        [Header("HUD Durante a Partida")]
        [SerializeField] private Image[] slotsFosseisHUD;          // 3 imagens do HUD (3 estados)
        [SerializeField] private Sprite spriteHUDNaoEncontrado;
        [SerializeField] private Sprite spriteHUDFoiEncontrado;
        [SerializeField] private Sprite spriteHUDDanificado;
        [SerializeField] private GameObject fossilQuebrouFeedback;
        [SerializeField] private GameObject fossilEncontradoFeedback;

        // --- Animações HUD ---
        private enum EstadoFossilHUD : byte { NaoEncontrado, Encontrado, Danificado }

        [Header("Animações HUD (tweakables)")]
        [SerializeField] private float popDurUp = 0.18f;
        [SerializeField] private float popDurDown = 0.14f;
        [SerializeField] private float shakeDur = 0.25f;
        [SerializeField] private float shakeAmp = 10f;   // pixels

        private EstadoFossilHUD[] estadoSlotsHUD; // estado atual de cada slot
        private Coroutine[] rotinasHUD;           // animação em andamento por slot
        private Vector2[] basePosHUD;             // posição base pra reset
        private Vector3[] baseScaleHUD;           // escala base pra reset
        private Color[] baseColorHUD;             // cor base pra reset

        private ControladorFerramenta controladorFerramentas;
        private int quantidadeAcoesInicial = 0;

        // --- Animações HUD ---
        [Header("Ferramenta inicial")]
        [SerializeField] private TrocarFerramentaViaBotaoUI primeiraFerramenta;

        /// Getters / Setters
        public AtualizarValorSlider UiSliderTempo => uiSliderTempo;
        public Action AoFinalizarPartida;

        [SerializeField] private GameObject[] estagios;
        private int lvlCarregado = 0;

        void Start()
        {
            CriarEstagio();

            controladorFerramentas = GetComponent<ControladorFerramenta>();

            quantidadeAcoesInicial = acoesParaFimJogo;
            controladorFerramentas.EventoAposRealizarUsoFerramenta += verificarFimFosseis;
            controladorFerramentas.EventoAposRealizarUsoFerramenta += verificarFimSemAcoes;

            // Contar fósseis na cena
            BlocoGenerico[] blocosGenericosNaCena = FindObjectsOfType<BlocoGenerico>();
            fosseisParaColetar = 0;
            foreach (var item in blocosGenericosNaCena)
            {
                if (item.BlocoSO.Tipo == NaturezaBlocoFerramenta.TipoBloco.FOSSIL)
                    fosseisParaColetar++;
            }

            // Estado inicial do botão (escondido até o fim)
            if (botaoTentarNovamente != null) botaoTentarNovamente.SetActive(false);

            // Inicializa HUD + baseline visual e estados
            InicializarHUD();

            // Inicializa HUD com 0 encontrados/0 danificados
            atualizarHUDDeFosseis();

            uiSliderTempo.atualizarValorSlider(acoesParaFimJogo, quantidadeAcoesInicial);
            foreach (TrocarFerramentaViaBotaoUI item in FindObjectsOfType<TrocarFerramentaViaBotaoUI>())
            {
                item.eventoTentativaTrocaFerramenta += uiSliderTempo.atualizarSlider;
            }

            Invoke("equiparPrimeiraFerramenta", 0.1f); // aguarda um frame para garantir que tudo esteja ok
        }

        private void equiparPrimeiraFerramenta()
        {
            primeiraFerramenta.forcarTrocaFerramenta();
        }

        private void atualizarProfessora()
        {
            if (imagemProfessora == null) return;

            int qtd = Mathf.Clamp(fosseisColetados, 0, 3);

            Sprite alvo =
                (qtd >= 3) ? professoraCompleto :
                (qtd >= 1) ? professoraParcial :
                             professoraNenhum;

            imagemProfessora.sprite = alvo;
            imagemProfessora.enabled = (alvo != null); // evita ficar vazia
        }

        public void CriarEstagio()
        {
            lvlCarregado = GerenciadorDados.Instance.levelSelecionado;
            Instantiate(estagios[lvlCarregado]);
        }

        public void verificarFimFosseis(ResumoInteracaoBlocoFerramenta resumo)
        {
            // Atualiza contadores
            fosseisColetados += resumo.QuantidadeFossilColetado;
            fosseisDanificados += resumo.QuantidadeFossilDestruido;

            // Limita para não passar do total de slots (ex.: 3)
            int meta = (slotsFosseisHUD != null && slotsFosseisHUD.Length > 0) ? slotsFosseisHUD.Length : 3;
            fosseisColetados = Mathf.Clamp(fosseisColetados, 0, meta);
            fosseisDanificados = Mathf.Clamp(fosseisDanificados, 0, meta - fosseisColetados);

            // Ajusta restante para encerrar quando não há mais o que coletar
            fosseisParaColetar -= resumo.QuantidadeFossilColetado + resumo.QuantidadeFossilDestruido;

            // Atualiza HUD em tempo real (com animação de transição)
            atualizarHUDDeFosseis();

            if (fosseisParaColetar <= 0)
                finalizarPartida();
        }

        public void verificarFimSemAcoes(ResumoInteracaoBlocoFerramenta resumo)
        {
            acoesParaFimJogo -= resumo.FerramentaUsada.TempoGastoAposUso;
            acoesParaFimJogo = Math.Min(acoesParaFimJogo + acoesGanhasPorAmbar * resumo.QuantidadeAmbarColetado, quantidadeAcoesInicial);
            uiSliderTempo.atualizarValorSlider(acoesParaFimJogo, quantidadeAcoesInicial);

            if (acoesParaFimJogo <= 0)
                finalizarPartida();
        }

        public void finalizarPartida()
        {
            GerenciadorDados.Instance.jogoFinalizado = true;
            telaFinal.SetActive(true);

            salvarFosseisColetados();
            atualizarSlotsFosseis();      // <- agora usa sprites da HUD (3 estados)
            atualizarMensagemFinalEAcao();
            atualizarProfessora();

            AoFinalizarPartida?.Invoke();
        }

        private void salvarFosseisColetados()
        {
            int indiceFossilInicial = 1 + lvlCarregado * 3;
            for (int i = 0; i < fosseisColetados; i++)
                SalvarCarregar.Instance.MarcarFossilEncontrado(indiceFossilInicial + i);

            if (fosseisColetados > 0)
                SalvarCarregar.Instance.MarcarNivelCompleto(lvlCarregado);
        }

        // --- UI Helpers (Final) ---

        private void atualizarSlotsFosseis()
        {
            if (slotsFosseis == null || slotsFosseis.Length == 0) return;

            int totalFinal = slotsFosseis.Length;
            int encontrados = Mathf.Clamp(fosseisColetados, 0, totalFinal);
            int danificados = Mathf.Clamp(fosseisDanificados, 0, totalFinal - encontrados);

            for (int i = 0; i < totalFinal; i++)
            {
                var img = slotsFosseis[i];
                if (img == null) continue;

                if (i < encontrados)
                    img.sprite = spriteHUDFoiEncontrado;   // mesmo sprite da HUD
                else if (i < encontrados + danificados)
                    img.sprite = spriteHUDDanificado;       // mesmo sprite da HUD
                else
                    img.sprite = spriteHUDNaoEncontrado;    // mesmo sprite da HUD
            }
        }

        private void atualizarMensagemFinalEAcao()
        {
            if (mensagemFinal == null) return;

            int meta = (slotsFosseis != null && slotsFosseis.Length > 0) ? slotsFosseis.Length : 3;
            int qtd = Mathf.Clamp(fosseisColetados, 0, meta);

            if (qtd == 0)
            {
                mensagemFinal.text = "Nenhum fóssil desta vez… Sem problema! Vamos tentar novamente?";
            }
            else if (qtd < meta)
            {
                mensagemFinal.text = $"Muito bem! {qtd} {(qtd == 1 ? "fóssil" : "fósseis")} para o nosso laboratório! Continue assim!";
            }
            else // qtd == meta (coleção completa)
            {
                mensagemFinal.text = "Brilhante! Você pegou os três fósseis! Estou orgulhosa de você!";
            }

            if (botaoTentarNovamente != null)
                botaoTentarNovamente.SetActive(qtd == 0);
        }

        // --- Inicialização HUD e baseline ---
        private void InicializarHUD()
        {
            int total = (slotsFosseisHUD != null) ? slotsFosseisHUD.Length : 0;
            estadoSlotsHUD = new EstadoFossilHUD[Mathf.Max(total, 0)];
            rotinasHUD = new Coroutine[Mathf.Max(total, 0)];
            basePosHUD = new Vector2[Mathf.Max(total, 0)];
            baseScaleHUD = new Vector3[Mathf.Max(total, 0)];
            baseColorHUD = new Color[Mathf.Max(total, 0)];

            for (int i = 0; i < total; i++)
            {
                estadoSlotsHUD[i] = EstadoFossilHUD.NaoEncontrado;

                if (slotsFosseisHUD[i] != null)
                {
                    var img = slotsFosseisHUD[i];
                    var rt = img.rectTransform;

                    basePosHUD[i] = rt.anchoredPosition;
                    baseScaleHUD[i] = Vector3.one;
                    baseColorHUD[i] = img.color;

                    // sprite e reset visual
                    if (spriteHUDNaoEncontrado != null)
                        img.sprite = spriteHUDNaoEncontrado;
                    rt.localScale = Vector3.one;
                }
            }
        }

        // --- UI Helpers (HUD em tempo real com animação) ---
        private void atualizarHUDDeFosseis()
        {
            if (slotsFosseisHUD == null || slotsFosseisHUD.Length == 0) return;

            int total = slotsFosseisHUD.Length;

            // Garante consistência (encontrados + danificados <= total)
            int encontrados = Mathf.Clamp(fosseisColetados, 0, total);
            int danificados = Mathf.Clamp(fosseisDanificados, 0, total - encontrados);

            for (int i = 0; i < total; i++)
            {
                var img = slotsFosseisHUD[i];
                if (img == null) continue;

                // Qual deve ser o novo estado deste slot?
                EstadoFossilHUD novoEstado =
                    (i < encontrados) ? EstadoFossilHUD.Encontrado :
                    (i < encontrados + danificados) ? EstadoFossilHUD.Danificado :
                                                       EstadoFossilHUD.NaoEncontrado;

                // Se não mudou, não anima nem mexe em sprite
                if (estadoSlotsHUD != null && i < estadoSlotsHUD.Length && novoEstado == estadoSlotsHUD[i])
                    continue;

                // Atualiza sprite conforme o novo estado + anima
                switch (novoEstado)
                {
                    case EstadoFossilHUD.Encontrado:
                        if (spriteHUDFoiEncontrado != null) img.sprite = spriteHUDFoiEncontrado;
                        DispararAnimEncontrado(i, img);
                        break;

                    case EstadoFossilHUD.Danificado:
                        if (spriteHUDDanificado != null) img.sprite = spriteHUDDanificado;
                        DispararAnimDanificado(i, img);
                        break;

                    default: // NaoEncontrado
                        if (spriteHUDNaoEncontrado != null) img.sprite = spriteHUDNaoEncontrado;
                        PararAnim(i, img, resetVisual: true);
                        break;
                }

                if (estadoSlotsHUD != null && i < estadoSlotsHUD.Length)
                    estadoSlotsHUD[i] = novoEstado;
            }
        }

        private void PararAnim(int idx, Image img, bool resetVisual)
        {
            if (rotinasHUD != null && idx >= 0 && idx < rotinasHUD.Length && rotinasHUD[idx] != null)
            {
                StopCoroutine(rotinasHUD[idx]);
                rotinasHUD[idx] = null;
            }
            if (resetVisual && img != null && basePosHUD != null && baseScaleHUD != null && baseColorHUD != null)
            {
                var rt = img.rectTransform;
                if (idx >= 0 && idx < basePosHUD.Length) rt.anchoredPosition = basePosHUD[idx];
                if (idx >= 0 && idx < baseScaleHUD.Length) rt.localScale = baseScaleHUD[idx];
                if (idx >= 0 && idx < baseColorHUD.Length) img.color = baseColorHUD[idx];
            }
        }

        private void DispararAnimEncontrado(int idx, Image img)
        {
            PararAnim(idx, img, resetVisual: true);
            rotinasHUD[idx] = StartCoroutine(AnimPop(img));
            fossilEncontradoFeedback.SetActive(true);
        }

        private void DispararAnimDanificado(int idx, Image img)
        {
            PararAnim(idx, img, resetVisual: true);
            rotinasHUD[idx] = StartCoroutine(AnimCrack(img));
            fossilQuebrouFeedback.SetActive(true);
        }

        private IEnumerator AnimPop(Image img)
        {
            if (img == null) yield break;
            var rt = img.rectTransform;
            var baseScale = Vector3.one;
            var c0 = img.color;
            Color flash = new Color(1f, 1f, 1f, c0.a);

            // fase up (0.85 -> 1.18)
            float t = 0f;
            while (t < popDurUp)
            {
                t += Time.unscaledDeltaTime;
                float k = t / popDurUp;
                float s = Mathf.SmoothStep(0.85f, 1.18f, k);
                rt.localScale = baseScale * s;
                img.color = Color.Lerp(c0, flash, 0.35f * k);
                yield return null;
            }

            // fase down (1.18 -> 1.00)
            t = 0f;
            while (t < popDurDown)
            {
                t += Time.unscaledDeltaTime;
                float k = t / popDurDown;
                float s = Mathf.SmoothStep(1.18f, 1.0f, k);
                rt.localScale = baseScale * s;
                img.color = Color.Lerp(flash, c0, k);
                yield return null;
            }

            rt.localScale = baseScale;
            img.color = c0;
        }

        private IEnumerator AnimCrack(Image img)
        {
            if (img == null) yield break;
            var rt = img.rectTransform;
            var basePos = rt.anchoredPosition;
            var baseScale = rt.localScale;
            var c0 = img.color;

            Color hit = new Color(1f, 0.5f, 0.5f, c0.a);

            float t = 0f;
            while (t < shakeDur)
            {
                t += Time.unscaledDeltaTime;
                float k = t / shakeDur;
                float damper = 1f - k; // decai ao final
                float x = (Mathf.PerlinNoise(t * 60f, 0f) - 0.5f) * 2f * shakeAmp * damper;
                float y = (Mathf.PerlinNoise(0f, t * 60f) - 0.5f) * 2f * (shakeAmp * 0.5f) * damper;

                rt.anchoredPosition = basePos + new Vector2(x, y);
                rt.localScale = baseScale * (1f - 0.04f * (1f - damper)); // pequena contração
                img.color = Color.Lerp(c0, hit, 0.6f * damper);
                yield return null;
            }

            // restaura
            rt.anchoredPosition = basePos;
            rt.localScale = baseScale;
            img.color = c0;
        }

        // Botão "Menu"
        public void VoltarParaMenu()
        {
            animacaoCarregamento.SetTrigger("carregar");
        }

        // Botão "Tentar novamente"
        public void TentarNovamente()
        {
            // Recarrega a cena atual
            Scene ativa = SceneManager.GetActiveScene();
            GerenciadorDados.Instance.jogoFinalizado = false;
            SceneManager.LoadScene(ativa.buildIndex);
        }
    }
}
