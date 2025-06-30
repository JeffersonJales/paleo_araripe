using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.Client.Commands.WkTree.WorkspaceTreeNode;

namespace PaleoAraripe
{
    public class UtilitariosInput : MonoBehaviour
    {
        public static readonly int MOUSE_BOTAO_ESQUERDO = 0;
        public static readonly int MOUSE_BOTAO_DIREITO = 1;
        public static readonly bool isSmartphone = true; // Application.isMobilePlatform;

        public static bool LiberarCamera()
        {
            return isSmartphone ? 
                Input.GetMouseButton(MOUSE_BOTAO_DIREITO) : 
                Input.GetMouseButton(MOUSE_BOTAO_DIREITO);
        }

        public static bool InteracaoUsarFerramenta()
        {
            return isSmartphone ? 
                false : 
                Input.GetMouseButtonDown(MOUSE_BOTAO_ESQUERDO);
        }

        public static Ray ObterRaioProcurarBloco(Camera camera)
        {
            return isSmartphone ?
                new Ray(camera.transform.position, camera.transform.forward) :
                camera.ScreenPointToRay(Input.mousePosition);
        }

    }
}
