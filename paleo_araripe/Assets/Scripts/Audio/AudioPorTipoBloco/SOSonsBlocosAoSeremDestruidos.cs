using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe
{

    [CreateAssetMenu(fileName = "SomBlocoSO", menuName = "ScriptableObjects/SomBloco", order = 1)]
    public class SOSonsBlocosAoSeremDestruidos : ScriptableObject
    {
        [Range(0, 10)] public int prioridade = 0;
        [Range(0f, 1f)] public int volumeBase = 1;
        public List<AudioClip> audios = new List<AudioClip>();
        public NaturezaBlocoFerramenta.TipoSomAoSerDestruido tipoSonoro = NaturezaBlocoFerramenta.TipoSomAoSerDestruido.NULO;
    }
}
