using UnityEngine;

namespace PaleoAraripe {

[CreateAssetMenu(fileName = "NovaConfiguracaoEfeito", menuName = "ScriptableObjects/Efeitos/Configuracao Efeito")]
public class ConfiguracaoEfeito : ScriptableObject
{
    [Header("Configurações do Tremor")]
    [Range(0f, 0.5f)]
    [SerializeField] private float intensidadeTremor = 0.1f;
    [Range(0f, 1f)]
    [SerializeField] private float duracaoTremor = 0.3f;

    [Header("Configurações de Cooldown")]
    [Range(0f, 2f)]
    [SerializeField] private float cooldown = 0.5f;

    [Header("Configurações de Layer")]
    [SerializeField] private LayerMask layerMask;

    // Propriedades públicas para acessar os valores
    public float IntensidadeTremor => intensidadeTremor;
    public float DuracaoTremor => duracaoTremor;
    public float Cooldown => cooldown;
    public LayerMask LayerMask => layerMask;
}

} 