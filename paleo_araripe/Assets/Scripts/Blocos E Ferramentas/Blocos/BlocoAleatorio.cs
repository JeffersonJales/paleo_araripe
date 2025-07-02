using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe {
    /// <summary>
    /// Classe responsável por representar um bloco que pode se transformar aleatoriamente em outros blocos possíveis.
    /// Gerencia a troca e destruição do bloco atual conforme as interações do jogo.
    /// </summary>
    public class BlocoAleatorio : BlocoGenerico
    {
        [SerializeField] private List<GameObject> blocosPossiveis = new List<GameObject>();
        [SerializeField] private GameObject blocoAtual;

        private int posicaoAtual = 0;

        public override void Awake()
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

        public void Start()
        {
            UtilitariosGamePlay.ouvirResumoInteracaoFerramentaBloco(modificarBloco);
            if (blocosPossiveis.Count > 0)
                instanciarBloco();
            else
                Destroy(gameObject);
        }

        public void OnDestroy()
        {
            UtilitariosGamePlay.pararOuvirResumoInteracaoFerramentaBloco(modificarBloco);
        }

        private void modificarBloco(ResumoInteracaoBlocoFerramenta resumo)
        {
            if (resumo.BlocosDestruidos.Contains(blocoAtual))
                Destroy(gameObject);
            else
                instanciarBloco();
        }

        private void instanciarBloco()
        {
            
            if (blocoAtual != null) { 
                transform.position = blocoAtual.transform.position;
                Destroy(blocoAtual);
            }

            blocoAtual = Instantiate(blocosPossiveis[posicaoAtual], transform.position, transform.rotation);

            BlocoGenerico bg = blocoAtual.GetComponent<BlocoGenerico>();
            bg.Nome = Nome;
            bg.Descricao = Descricao;

            if (++posicaoAtual >= blocosPossiveis.Count)
                posicaoAtual = 0;
        }

    }
}
