using UnityEngine;

namespace PaleoAraripe {

/// <summary>
/// ScriptableObject que armazena configurações de efeitos visuais, como intensidade e duração do tremor.
/// </summary>
[CreateAssetMenu(fileName = "NovaConfiguracaoEfeito", menuName = "ScriptableObjects/Efeitos/Configuracao Efeito")]
    public class ConfiguracaoEfeito : ScriptableObject
    {
        [Header("Configurações do Tremor")]
        [Range(0f, 0.5f)]
        [SerializeField] private float intensidadeTremor = 0.1f;
        [Range(0f, 1f)]
        [SerializeField] private float duracaoTremor = 0.3f;

        // Propriedades públicas para acessar os valores
        public float IntensidadeTremor => intensidadeTremor;
        public float DuracaoTremor => duracaoTremor;
    }
} 