using UnityEngine;
using Cinemachine;
namespace PaleoAraripe
{
    public class ControleCamera : MonoBehaviour
    {
        private CinemachineFreeLook freeLookCamera;
        [SerializeField] private float velocidadeX = 300f;
        [SerializeField] private float velocidadeY = 2f;

        void Start()
        {
            freeLookCamera = FindAnyObjectByType<CinemachineFreeLook>();
        }

        void Update()
        {
            AtualizaVelocidadeCamera();
        }

        void AtualizaVelocidadeCamera()
        {
            if (UtilitariosInput.LiberarCamera() && !GerenciadorDados.Instance.jogoFinalizado)
            {
                freeLookCamera.m_XAxis.m_MaxSpeed = velocidadeX;
                freeLookCamera.m_YAxis.m_MaxSpeed = velocidadeY;
            }
            else
            {
                freeLookCamera.m_XAxis.m_MaxSpeed = 0f;
                freeLookCamera.m_YAxis.m_MaxSpeed = 0f;
            }
        }
    }
}
