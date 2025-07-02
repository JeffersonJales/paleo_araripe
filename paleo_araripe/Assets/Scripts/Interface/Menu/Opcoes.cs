using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace PaleoAraripe {
    /// <summary>
    /// Classe responsável pelo menu de opções/configurações do jogo, permitindo ajuste de volume, gráficos, fonte e destaque de arestas.
    /// </summary>
    public class Opcoes : MonoBehaviour
    {
        [SerializeField] private ConfiguracoesSO configuracoes;
        [SerializeField] private GameObject canvasConfiguracao;
        [SerializeField] private GameObject canvasPrincipal;
        [SerializeField] private Slider sliderMusica;
        [SerializeField] private Slider sliderSFX;
        [SerializeField] private TMP_Dropdown dropdownGrafico;
        [SerializeField] private TMP_Dropdown dropdownFonte;
        [SerializeField] private Toggle arestasDestacadas;

        private void Start()
        {
            configuracoes.CarregarConfiguracoes();
            dropdownGrafico.value = configuracoes.QualidadeVisual;
            sliderMusica.value = configuracoes.VolumeMusica;
            sliderSFX.value = configuracoes.VolumeSFX;
            dropdownFonte.value = configuracoes.IncrementoFonte;
            arestasDestacadas.isOn = configuracoes.ModoArestaDestacada;
            dropdownGrafico.RefreshShownValue();
            dropdownFonte.RefreshShownValue();
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
            configuracoes.QualidadeVisual = dropdownGrafico.value;
        }

        public void AplicarTamanhoFonte()
        {
            // O valor do dropdown pode ser normal, que irá manter a fonte padrão, e grande que irá incrementar a fonte
            configuracoes.IncrementoFonte = dropdownFonte.value;
        }

        public void AplicarArestasDestacadas()
        {
            // Alterna o modo de destaque das arestas entre ativado e desativado
            // através do toggle no menu de configurações
            configuracoes.ModoArestaDestacada = !configuracoes.ModoArestaDestacada;
        }

        public void Voltar()
        {
            // Fecha o menu de configurações e retorna para o menu principal
            canvasConfiguracao.SetActive(false);
            canvasPrincipal.SetActive(true);
        }
        public void Salvar()
        {
            QualitySettings.SetQualityLevel(configuracoes.QualidadeVisual);
            SetarTamanhoFonte.mudarTamanhoFonte.Invoke(configuracoes.IncrementoFonte == 0);
            UtilitarioAudio.SetarVolumeMusica(sliderMusica.value);
            UtilitarioAudio.SetarVolumeSFX(sliderSFX.value);
            PlayerPrefs.Save();
            canvasConfiguracao.SetActive(false);
            canvasPrincipal.SetActive(true);
        }
    }
}
