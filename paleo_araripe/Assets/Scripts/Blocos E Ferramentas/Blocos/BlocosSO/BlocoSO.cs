using System;
using UnityEngine;

namespace PaleoAraripe
{
    [CreateAssetMenu(fileName = "BlocoSO", menuName = "ScriptableObjects/Blocos", order = 2)]
    public class BlocoSO : ScriptableObject
    {
        [Range(1, 5)] 
        [SerializeField] private int vida = 1;
        [SerializeField] private bool sofreDanoQuandoCuboCaiNele = false;

        [SerializeField] private NaturezaBlocoFerramenta.TipoBloco tipo = NaturezaBlocoFerramenta.TipoBloco.NORMAL;
    
        [SerializeField] private GameObject feedbackAoDestruir;
        [SerializeField] private GameObject feedbackAoColetar;
        [SerializeField] private GameObject feedbackAoTomarDano;
        [SerializeField] private ConfiguracaoEfeito configuracaoEfeitoVisual;

        public int Vida => vida;

        public NaturezaBlocoFerramenta.TipoBloco Tipo => tipo;

        public GameObject FeedbackAoColetar => feedbackAoColetar;
        public GameObject FeedbackAoDestruir => feedbackAoDestruir;
        public GameObject FeedbackAoTomarDano => feedbackAoTomarDano;
        public ConfiguracaoEfeito ConfiguracaoEfeitoVisual => configuracaoEfeitoVisual;
        public bool SofreDanoQuandoCuboCaiNele => sofreDanoQuandoCuboCaiNele;
    }
}

