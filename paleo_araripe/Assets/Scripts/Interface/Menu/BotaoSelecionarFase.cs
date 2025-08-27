using UnityEngine;
using UnityEngine.UI;

namespace PaleoAraripe
{
    public class BotaoSelecionarFase : MonoBehaviour
    {
        [SerializeField] private int fase = 0;

        void OnEnable()
        {
            SalvarCarregar.Instance.Carregar(out bool[] niveisCompletos, out bool[] fosseisEncontrados);
            Button button = GetComponent<Button>();

            if (niveisCompletos[fase - 1])
            {
                button.enabled = true;
            }
            else
            {
                button.enabled = false;
            }
        }
    }
}
