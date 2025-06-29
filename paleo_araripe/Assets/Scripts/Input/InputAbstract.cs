using UnityEngine;

namespace PaleoAraripe
{
    public abstract class InputAbstract : MonoBehaviour
    {
        protected Camera camera;

        public void Start()
        {
            camera = Camera.main;
        }

        public abstract bool PodeMoverCamera();
        public abstract bool RealizarInteracao();
        public abstract Ray PreviewInteracao();
    }
}
