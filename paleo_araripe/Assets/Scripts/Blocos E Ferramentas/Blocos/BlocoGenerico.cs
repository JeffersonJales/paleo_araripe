using System;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace PaleoAraripe
{
    public class BlocoGenerico : MonoBehaviour
    {
        [SerializeField] private string nome = "Nome Bloco";
        [SerializeField] private string descricao = "Descricao Bloco";
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
        public string Nome => nome;
        public string Descricao => descricao;

        private string corSelecionada = "D6D6D6";
        private string corNormal = "FFFFFF";


        public virtual void Awake()
        {
            vidaAtual = BlocoSO.Vida;

            efeitoDeDano = GetComponent<BlockHitEffect>();

            animatorBloco = GetComponent<Animator>();

            bc = objetoBloco.GetComponent<BoxCollider>();

            mr = objetoBloco.GetComponent<MeshRenderer>();
            if (ColorUtility.TryParseHtmlString(corNormal, out Color corFinal))
            {
                mr.material.SetColor("_BaseColor", corFinal);
            }
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

            if (ColorUtility.TryParseHtmlString(corSelecionada, out Color corFinal))
            {
                mr.material.SetColor("_BaseColor", corFinal);
            }
        }

        public void casoDeixeDeSerFocoDaFerramenta()
        {
            if (!emFoco) return;
            emFoco = false;

            if (ColorUtility.TryParseHtmlString(corNormal, out Color corFinal))
            {
                mr.material.SetColor("_BaseColor", corFinal);
            }
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