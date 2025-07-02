using UnityEngine;

namespace PaleoAraripe {
    /// <summary>
    /// Classe que define o comportamento do bloco de lava líquida, que pode se solidificar após alguns turnos.
    /// </summary>
    public class BlocoLavaLiquido : BlocoGenerico
    {
        [SerializeField] private GameObject refBlocoPedra;
        [SerializeField] private int turnosParaLavaSecar = 3;

        public void Start()
        {
            UtilitariosGamePlay.ouvirResumoInteracaoFerramentaBloco(verificarPassagemTempoEndurecer);
        }


        public void OnDestroy()
        {
            UtilitariosGamePlay.pararOuvirResumoInteracaoFerramentaBloco(verificarPassagemTempoEndurecer);
        }

        public void verificarPassagemTempoEndurecer(ResumoInteracaoBlocoFerramenta resumo)
        {
            if (--turnosParaLavaSecar < 0)
            {
                Instantiate(refBlocoPedra, transform.position, Quaternion.identity, transform.parent);
                Destroy(gameObject);
            }
        }
    }
}
