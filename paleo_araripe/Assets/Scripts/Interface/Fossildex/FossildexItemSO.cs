using UnityEngine;
using UnityEngine.Serialization;

namespace PaleoAraripe
{
    /// <summary>
    /// Dados de um fóssil na Fossildex.
    /// - ImagemGrande: sprite do painel (ex.: "img-fossil-1")
    /// - IconeBotao: sprite do botão (ex.: "fossil-1")
    /// </summary>
    [CreateAssetMenu(fileName = "ItemFossildex", menuName = "Fossildex")]
    public class FossildexItemSO : ScriptableObject
    {
        [SerializeField] private int fossilID;
        [SerializeField] private string titulo;
        [TextArea(2, 6)][SerializeField] private string descricao;

        [Header("Sprites")]
        [FormerlySerializedAs("imagem")]
        [SerializeField] private Sprite imagemGrande;   // Antes era "imagem"
        [SerializeField] private Sprite iconeBotao;     // Novo: ícone do botão

        public int FossilID => fossilID;
        public string Titulo => titulo;
        public string Descricao => descricao;
        public Sprite ImagemGrande => imagemGrande;
        public Sprite IconeBotao => iconeBotao;
    }
}
