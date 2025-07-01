using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PaleoAraripe {
    public class Opcoes : MonoBehaviour
    {
        [SerializeField] private ConfiguracoesSO configuracoes;
        [SerializeField] private GameObject canvasConfiguracao;
        [SerializeField] private GameObject canvasPrincipal;
        [SerializeField] private GameObject modalAcessibilidade;
        [SerializeField] private Slider sliderMusica;
        [SerializeField] private Slider sliderSFX;
        [SerializeField] private TMP_Dropdown dropdownGrafico;
        [SerializeField] private TMP_Dropdown dropdownFonte;
        [SerializeField] private Toggle arestasDestacadas;
        [SerializeField] private Toggle blocosDestacados;

        private void Start()
        {
            // Inicializa os sliders com os valores salvos
            sliderMusica.value = configuracoes.VolumeMusica;
            sliderSFX.value = configuracoes.VolumeSFX;
        }

        public void AplicarVolumeMusica()
        {
            // Atualiza o volume da música com o valor do slider (0-100)
            // e dispara um evento para notificar outras classes
            configuracoes.VolumeMusica = sliderMusica.value;
        }

        public void AplicarVolumeSFX() 
        { 
            // Atualiza o volume dos efeitos sonoros com o valor do slider (0-100)
            // e dispara um evento para notificar outras classes
            configuracoes.VolumeSFX = sliderSFX.value;
        }

        public void AplicarModoGrafico() 
        {
            // 0 = Performance (Baixo)
            // 1 = Balanced (Medio)
            // 2 = Gráfico (Alto)
            int modoSelecionado = dropdownGrafico.value;
            
            switch (modoSelecionado)
            {
                case 0: // Performance
                    QualitySettings.SetQualityLevel(0); // Baixo
                    break;
                case 1: // Balanced
                    QualitySettings.SetQualityLevel(1); // Medio
                    break;
                case 2: // Gráfico
                    QualitySettings.SetQualityLevel(2); // Alto
                    break;
            }
        }

        public void AplicarTamanhoFonte()
        {
            // O valor do dropdown pode ser normal, que irá manter a fonte padrão, e grande que irá incrementar a fonte em 5 pontos
            if (dropdownFonte.value == 0)
                configuracoes.IncrementoFonte = 0;
            else
                configuracoes.IncrementoFonte = 5;
        }

        public void AplicarArestasDestacadas()
        {
            // Alterna o modo de destaque das arestas entre ativado e desativado
            // através do toggle no menu de configurações
            configuracoes.ModoArestaDestacada = !configuracoes.ModoArestaDestacada;
        }

        public void AplicarBlocosDestacados()
        {
            // Alterna o modo de destaque dos blocos entre ativado e desativado
            // através do toggle no menu de configurações
            configuracoes.ModoBlocoDestacado = !configuracoes.ModoBlocoDestacado;
        }

        public void AbrirModalAcessibilidade(int típoAcessibilidade)
        {
            // Abre o modal de acessibilidade com base no tipo selecionado
            // e exibe as opções específicas para cada tipo
            modalAcessibilidade.SetActive(true);
        }

        public void Voltar()
        {
            // Fecha o menu de configurações e retorna para o menu principal
            canvasConfiguracao.SetActive(false);
            canvasPrincipal.SetActive(true);
        }
    }
}
