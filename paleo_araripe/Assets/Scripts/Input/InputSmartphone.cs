using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe
{
    public class InputSmartphone : InputAbstract
    {
        public override bool PodeMoverCamera()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    return true;
                }
            }

            return false;
        }

        public override Ray PreviewInteracao()
        {
            return camera.ScreenPointToRay(Vector3.forward);
        }

        public override bool RealizarInteracao()
        {
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        }
    }
}
