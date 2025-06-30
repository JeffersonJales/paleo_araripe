using UnityEngine;
using Cinemachine;
namespace PaleoAraripe
{
    public class ControleCamera : MonoBehaviour
    {
        private bool podeMover;
        private CinemachineBrain cameraBrain;

        void Start()
        {
            cameraBrain = GetComponent<CinemachineBrain>();
        }

        void Update()
        {
            if (!GerenciadorDados.Instance.jogoFinalizado)
                MovimentoMouse();
            
            CameraPodeMover();
        }
        void CameraPodeMover()
        {
            cameraBrain.enabled = podeMover;
        }
        void MovimentoMouse()
        {
            podeMover = UtilitariosInput.LiberarCamera();
        }
    }
}
