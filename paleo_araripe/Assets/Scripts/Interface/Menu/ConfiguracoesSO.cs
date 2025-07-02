using UnityEngine;
using System;

namespace PaleoAraripe {
    /// <summary>
    /// ScriptableObject responsável por gerenciar e persistir as configurações do jogo.
    /// Utiliza PlayerPrefs para salvar as configurações entre sessões.
    /// </summary>
    [CreateAssetMenu(fileName = "ConfiguracoesJogo", menuName = "Configuracoes/ConfiguracoesJogo")]
    public class ConfiguracoesSO : ScriptableObject
    {
        // Eventos para notificar mudanças nos volumes
        public event Action<float> OnVolumeMusicaChanged;
        public event Action<float> OnVolumeSFXChanged;

        // Chaves para salvar as configurações no PlayerPrefs
        private const string KEY_VOLUME_MUSICA = "VolumeMusica";
        private const string KEY_VOLUME_SFX = "VolumeSFX";
        private const string KEY_INCREMENTO_FONTE = "IncrementoFonte";
        private const string KEY_ARESTA_DESTACADA = "ArestaDestacada";
        private const string KEY_QUALIDADE_VISUAL = "QualidadeVisual";

        // Valores padrão das configurações
        private float _volumeMusica = 1f;
        private float _volumeSFX = 1f;
        private int _incrementoFonte = 0;
        private int _qualidadeVisual = 1;
        private bool _modoArestaDestacada = false;

        /// <summary>
        /// Propriedade para controlar o volume da música.
        /// Salva automaticamente no PlayerPrefs quando alterado.
        /// Dispara evento OnVolumeMusicaChanged para notificar outras classes.
        /// </summary>
        public float VolumeMusica
        {
            get => _volumeMusica;
            set
            {
                _volumeMusica = value;
                PlayerPrefs.SetFloat(KEY_VOLUME_MUSICA, _volumeMusica);
                PlayerPrefs.Save();
                OnVolumeMusicaChanged?.Invoke(_volumeMusica);
            }
        }

        /// <summary>
        /// Propriedade para controlar o volume dos efeitos sonoros.
        /// Salva automaticamente no PlayerPrefs quando alterado.
        /// Dispara evento OnVolumeSFXChanged para notificar outras classes.
        /// </summary>
        public float VolumeSFX
        {
            get => _volumeSFX;
            set
            {
                _volumeSFX = value;
                PlayerPrefs.SetFloat(KEY_VOLUME_SFX, _volumeSFX);
                PlayerPrefs.Save();
                OnVolumeSFXChanged?.Invoke(_volumeSFX);
            }
        }
        /// <summary>
        /// Propriedade para controlar a qualidade visual
        /// Salva automaticamente no PlayerPrefs quando alterado.
        /// Dispara evento OnVolumeMusicaChanged para notificar outras classes.
        /// </summary>
        public int QualidadeVisual
        {
            get => _qualidadeVisual;
            set
            {
                _qualidadeVisual = value;
                PlayerPrefs.SetInt(KEY_QUALIDADE_VISUAL, _qualidadeVisual);
                PlayerPrefs.Save();
            }
        }
        /// <summary>
        /// Propriedade para controlar o incremento do tamanho da fonte.
        /// Salva automaticamente no PlayerPrefs quando alterado.
        /// </summary>
        public int IncrementoFonte
        {
            get => _incrementoFonte;
            set
            {
                _incrementoFonte = value;
                PlayerPrefs.SetInt(KEY_INCREMENTO_FONTE, _incrementoFonte);
                PlayerPrefs.Save();
            }
        }

        /// <summary>
        /// Propriedade para controlar o modo de destaque das arestas.
        /// Salva automaticamente no PlayerPrefs quando alterado.
        /// </summary>
        public bool ModoArestaDestacada
        {
            get => _modoArestaDestacada;
            set
            {
                _modoArestaDestacada = value;
                PlayerPrefs.SetInt(KEY_ARESTA_DESTACADA, value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }

        /// <summary>
        /// Carrega todas as configurações salvas no PlayerPrefs.
        /// Se não houver configurações salvas, usa os valores padrão.
        /// </summary>
        public void CarregarConfiguracoes()
        {
            _qualidadeVisual = PlayerPrefs.GetInt(KEY_QUALIDADE_VISUAL, 1);
            _incrementoFonte = PlayerPrefs.GetInt(KEY_INCREMENTO_FONTE, 0);
            _volumeMusica = PlayerPrefs.GetFloat(KEY_VOLUME_MUSICA, 0);
            _volumeSFX = PlayerPrefs.GetFloat(KEY_VOLUME_SFX, 0);
            _modoArestaDestacada = PlayerPrefs.GetInt(KEY_ARESTA_DESTACADA, 0) == 1;
        }
    }
}
