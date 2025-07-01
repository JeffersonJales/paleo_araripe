using UnityEngine;
using System;

namespace PaleoAraripe
{
    /// <summary>
    /// ScriptableObject responsável por armazenar as informações dos itens da fossildex
    /// </summary>
    [CreateAssetMenu(fileName = "ItemFossildex", menuName = "Fossildex")]
    public class FossildexItemSO : ScriptableObject
    {
        [SerializeField] private int fossilID;
        [SerializeField] private string titulo;
        [SerializeField] private string descricao;
        [SerializeField] private Sprite imagem;

        public int FossilID => fossilID;
        public string Titulo => titulo;
        public string Descricao => descricao;
        public Sprite Imagem => imagem;
    }
}
