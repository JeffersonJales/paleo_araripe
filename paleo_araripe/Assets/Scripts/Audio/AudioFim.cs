using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe
{
    public class AudioFim : MonoBehaviour
    {
        [SerializeField] private AudioClip faseFinalizacao;
        //metodo chamado na animacao de finalizacao
        public void TocarMusicaFim()
        {
            UtilitarioAudio.TocarBGM(faseFinalizacao);
        }
    }
}
