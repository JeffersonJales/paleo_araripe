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
        [SerializeField] private BotaoSwitchComportamento graficoSwitch;
        [SerializeField] private BotaoSwitchComportamento fonteSwitch;
        [SerializeField] private Toggle arestasDestacadas;
        public static bool tamanhoFonteAtual = false; // false = Normal, true = Grande
        private void Start()
        {
            configuracoes.CarregarConfiguracoes();
            //QualidadeVisual e IncrementoFonte salvam na memoria como 0 ou 1, mas o switch trabalha com true ou false, entao 0 = false e 1 = true
            graficoSwitch.ConfigurarBotaoSwitch(configuracoes.QualidadeVisual == 1);
            fonteSwitch.ConfigurarBotaoSwitch(configuracoes.IncrementoFonte == 1);
            sliderMusica.value = configuracoes.VolumeMusica;
            sliderSFX.value = configuracoes.VolumeSFX;
            arestasDestacadas.isOn = configuracoes.ModoArestaDestacada;
            AplicarModoGrafico();
            AplicarTamanhoFonte();
            gameObject.SetActive(false);
        }
        public void AplicarVolumeMusica()
        {
            // Atualiza o volume da música com o valor do slider (0-100)
            // e dispara um evento para notificar outras classes
            configuracoes.VolumeMusica = sliderMusica.value;
            UtilitarioAudio.SetarVolumeMusica(sliderMusica.value);
        }

        public void AplicarVolumeSFX() 
        { 
            // Atualiza o volume dos efeitos sonoros com o valor do slider (0-100)
            // e dispara um evento para notificar outras classes
            configuracoes.VolumeSFX = sliderSFX.value;
            UtilitarioAudio.SetarVolumeSFX(sliderSFX.value);
        }

        public void AplicarModoGrafico() 
        {
            // 0 = Performance (Baixo)
            // 1 = Gráfico (Alto)
            configuracoes.QualidadeVisual = graficoSwitch.EstadoSwitch ? 1 : 0;
            QualitySettings.SetQualityLevel(configuracoes.QualidadeVisual);
        }

        public void AplicarTamanhoFonte()
        {
            // O valor do dropdown pode ser normal, que irá manter a fonte padrão, e grande que irá incrementar a fonte
            configuracoes.IncrementoFonte = fonteSwitch.EstadoSwitch ? 1 : 0;
            tamanhoFonteAtual = configuracoes.IncrementoFonte == 0;
            SetarTamanhoFonte.mudarTamanhoFonte.Invoke();
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
            PlayerPrefs.Save();
            canvasConfiguracao.SetActive(false);
            canvasPrincipal.SetActive(true);
        }
    }
}
