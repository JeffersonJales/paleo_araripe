using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe
{
    public class InputPC : InputAbstract
    {
        private const int BOTAO_MOUSE_ESQUERDO = 0;
        private const int BOTAO_MOUSE_DIREITO = 1;

        public override bool PodeMoverCamera()
        {
            return Input.GetMouseButton(BOTAO_MOUSE_DIREITO);
        }

        public override Ray PreviewInteracao()
        {
            return camera.ScreenPointToRay(Input.mousePosition);
        }

        public override bool RealizarInteracao()
        {
            return Input.GetMouseButtonDown(BOTAO_MOUSE_ESQUERDO);
        }
    }
}
