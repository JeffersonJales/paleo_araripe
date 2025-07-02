using System;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace PaleoAraripe
{
    /// <summary>
    /// Classe base para todos os blocos do jogo, contendo propriedades e métodos comuns de interação, dano, coleta e feedback visual.
    /// </summary>
    public class BlocoGenerico : MonoBehaviour
    {
        [SerializeField] private string nome = "Nome Bloco";
        [TextArea(5, 10)][SerializeField] private string descricao = "Descricao Bloco";

        [SerializeField] private BlocoSO blocoSO;
        [SerializeField] private GameObject objetoBloco;
        [SerializeField] private ConfiguracaoEfeito efeitoNenhum;
        [SerializeField] private GameObject particulaImune;

        private Vector3 pontoImpacto;
        private Animator animatorBloco;
        private int vidaAtual = 1;
        private bool emFoco = false;
        private bool emFocoCristal = false;
        private BlockHitEffect efeitoDeDano;

        protected MeshRenderer mr = null;
        protected BoxCollider bc = null;

        // Getters 
        public BlocoSO BlocoSO => blocoSO;
        public string Nome {get => nome; set => nome = value;} 
        public string Descricao { get => descricao; set => descricao = value; }

        [SerializeField] private Color corSelecionado = new Color(0.839f, 0.839f, 0.839f, 0);
        private Color corNaoSelecionado;

        public virtual void Awake()
        {
            vidaAtual = BlocoSO.Vida;
            efeitoDeDano = GetComponent<BlockHitEffect>();
            animatorBloco = GetComponent<Animator>();
            bc = objetoBloco.GetComponent<BoxCollider>();
            mr = objetoBloco.GetComponent<MeshRenderer>();

            corNaoSelecionado = mr.material.GetColor("_BaseColor");
        }
        public void SetPontoImpacto(Vector3 ponto)
        {
            pontoImpacto = ponto;
        }
        public void participarColisao(bool estado)
        {
            bc.enabled = estado;
        }

        public bool tomarDano(int qtdDano)
        {
            bool destruido = false;
            vidaAtual -= qtdDano;

            if (vidaAtual <= 0)
            {
                destruido = true;
                aoSerDestruido();
            }
            else
                aoTomarDano();

            return destruido;
        }

        public bool estaVivo()
        {
            return vidaAtual > 0 && bc.enabled;
        }
        public void DefinirVidaVisual()
        {
            float percent = (float)vidaAtual * 100f / (float)BlocoSO.Vida;
            int efeitoVida;
            if (percent >= 80f)
                efeitoVida = 0;
            else if (percent >= 60f)
                efeitoVida = 1;
            else if (percent >= 40f)
                efeitoVida = 2;
            else if (percent >= 20f)
                efeitoVida = 3;
            else
                efeitoVida = 4;
            animatorBloco.SetInteger("estado", efeitoVida);
            animatorBloco.SetTrigger("hit");
        }
        public virtual void aoSerImune()
        {
            efeitoDeDano.IniciarEfeito(blocoSO.ConfiguracaoEfeitoVisual, particulaImune, pontoImpacto);
        }
        public virtual void aoTomarDano()
        {
            efeitoDeDano.IniciarEfeito(blocoSO.ConfiguracaoEfeitoVisual, blocoSO.FeedbackAoTomarDano, pontoImpacto);
            DefinirVidaVisual();
            animatorBloco.SetTrigger("atingido");
        }

        public virtual void aoSerDestruido()
        {
            efeitoDeDano.IniciarEfeito(blocoSO.ConfiguracaoEfeitoVisual, blocoSO.FeedbackAoDestruir, pontoImpacto);
            animatorBloco.SetTrigger("quebrou");
        }

        public virtual void aoSerColetado()
        {
            efeitoDeDano.IniciarEfeito(efeitoNenhum, blocoSO.FeedbackAoColetar, pontoImpacto);
            animatorBloco.SetTrigger("quebrou");
        }


        #region Feedback Bloco � Alvo da ferramenta

        public void casoSejaFocoDaFerramenta()
        {
            if (emFoco) return;
            emFoco = true;

            mr.material.SetColor("_BaseColor", corSelecionado);
        }

        public void casoDeixeDeSerFocoDaFerramenta()
        {
            if (!emFoco) return;
            emFoco = false;

            mr.material.SetColor("_BaseColor", corNaoSelecionado);
        }

        #endregion

        #region Feedback Bloco alvo do cristal

        public void casoSejaFocoDoCristal()
        {
            emFocoCristal = true;
        }

        public void casoDeixeDeSerFocoDoCristal()
        {
            emFocoCristal = false;
        }

        #endregion
    }
}