using UnityEngine;
using UnityEngine.UI;

namespace PaleoAraripe
{
    public class UiFerramentaAtual : MonoBehaviour
    {
        private Image img;
        private Button btn;
        [SerializeField] private bool usarFerramentaBotao = false;

        void Start()
        {

            img = GetComponent<Image>();
            btn = GetComponent<Button>();

            btn.enabled = false;
            if(usarFerramentaBotao)
                btn.onClick.AddListener(UtilizarFerramentaEquipada);

            foreach (TrocarFerramentaViaBotaoUI item in FindObjectsOfType<TrocarFerramentaViaBotaoUI>())
            {
                item.eventoTentativaTrocaFerramenta += TrocouFerramenta;
            }
            UtilitariosGamePlay.ouvirResumoInteracaoFerramentaBloco(VerificarFerramenteFoiCongelada);
        }

        private void OnDestroy()
        {
            foreach (TrocarFerramentaViaBotaoUI item in FindObjectsOfType<TrocarFerramentaViaBotaoUI>())
            {
                item.eventoTentativaTrocaFerramenta -= TrocouFerramenta;
            }
        }

        private void UtilizarFerramentaEquipada()
        {
            UtilitariosGamePlay.UtilizarFerramentaEquipada();
        }

        private void TrocouFerramenta(FerramentaSO ferramentSo)
        {
            bool temFerramenta = ferramentSo != null;
            img.enabled = temFerramenta;
            btn.enabled = temFerramenta && UtilitariosInput.isSmartphone;

            if (!temFerramenta)
                return;

            img.sprite = ferramentSo.SpriteFerramenta;
        }
        
        private void VerificarFerramenteFoiCongelada(ResumoInteracaoBlocoFerramenta resumo)
        {
            if (resumo.FerramentaCongelada)
                btn.enabled = false;
        }
    }
}
