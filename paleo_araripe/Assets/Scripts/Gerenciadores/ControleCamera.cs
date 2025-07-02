using UnityEngine;
using Cinemachine;

namespace PaleoAraripe
{
    /// <summary>
    /// Classe responsável por gerenciar a movimentação da câmera orbital usando CinemachineFreeLook, com controle de rotação e bloqueio por input.
    /// Permite controlar a rotação da câmera apenas quando o botão direito do mouse está pressionado,
    /// bloqueando o movimento em outros momentos. Os valores de velocidade podem ser ajustados via Inspector.
    /// </summary>
    public class ControleCamera : MonoBehaviour
    {
        /// <summary>
        /// Referência para o componente CinemachineFreeLook na cena.
        /// </summary>
        private CinemachineFreeLook freeLookCamera;

        /// <summary>
        /// Velocidade de rotação horizontal (eixo X) da câmera. Ajustável pelo Inspector.
        /// </summary>
        [SerializeField] private float velocidadeX = 300f;

        /// <summary>
        /// Velocidade de rotação vertical (eixo Y) da câmera. Ajustável pelo Inspector.
        /// </summary>
        [SerializeField] private float velocidadeY = 2f;

        /// <summary>
        /// Inicializa a referência para o CinemachineFreeLook ao iniciar o script.
        /// </summary>
        void Start()
        {
            // Procura o CinemachineFreeLook na cena para controlar a câmera orbital.
            freeLookCamera = FindAnyObjectByType<CinemachineFreeLook>();
        }

        /// <summary>
        /// Atualiza a velocidade da câmera a cada frame, permitindo movimento apenas quando o botão direito do mouse está pressionado.
        /// </summary>
        void Update()
        {
            AtualizaVelocidadeCamera();
        }

        /// <summary>
        /// Controla a velocidade máxima dos eixos do CinemachineFreeLook.
        /// Se o botão direito do mouse estiver pressionado e o jogo não estiver finalizado,
        /// permite movimentação da câmera. Caso contrário, trava a câmera.
        /// </summary>
        void AtualizaVelocidadeCamera()
        {
            // UtilitariosInput.LiberarCamera() deve retornar true se o botão direito do mouse estiver pressionado.
            // GerenciadorDados.Instance.jogoFinalizado deve ser false para permitir movimentação.
            if (UtilitariosInput.LiberarCamera() && !GerenciadorDados.Instance.jogoFinalizado)
            {
                // Permite movimentação da câmera
                freeLookCamera.m_XAxis.m_MaxSpeed = velocidadeX;
                freeLookCamera.m_YAxis.m_MaxSpeed = velocidadeY;
            }
            else
            {
                // Trava a câmera
                freeLookCamera.m_XAxis.m_MaxSpeed = 0f;
                freeLookCamera.m_YAxis.m_MaxSpeed = 0f;
            }
        }
    }
}
