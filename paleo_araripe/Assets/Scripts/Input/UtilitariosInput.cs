using UnityEngine;

namespace PaleoAraripe
{
    /// <summary>
    /// Classe utilitária para centralizar e padronizar o acesso aos inputs do jogador, tanto para PC quanto para mobile.
    /// </summary>
    public class UtilitariosInput : MonoBehaviour
    {
        public static readonly int MOUSE_BOTAO_ESQUERDO = 0;
        public static readonly int MOUSE_BOTAO_DIREITO = 1;
        public static readonly bool isSmartphone = Application.isMobilePlatform;

        public static bool LiberarCamera()
        {
            return isSmartphone ? 
                Input.GetMouseButton(MOUSE_BOTAO_DIREITO) : 
                Input.GetMouseButton(MOUSE_BOTAO_DIREITO);
        }

        public static bool InteracaoUsarFerramenta()
        {
            return isSmartphone ?
                Input.GetMouseButtonDown(MOUSE_BOTAO_ESQUERDO) : 
                Input.GetMouseButtonDown(MOUSE_BOTAO_ESQUERDO);
        }

        public static Ray ObterRaioProcurarBloco(Camera camera)
        {
            return isSmartphone ?
                camera.ScreenPointToRay(Input.mousePosition):
                camera.ScreenPointToRay(Input.mousePosition);
        }


    }
}
