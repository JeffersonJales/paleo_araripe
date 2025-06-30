using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe
{
    public class UtilitarioAudio : MonoBehaviour
    {
        public static ControladorAudio ObterControladorAudio()
        {
            return FindObjectOfType<ControladorAudio>();
        }
        
        public static void TocarBGM(AudioClip audio)
        {
            ObterControladorAudio().TocarAudio(audio);
        }

        public static void TocarBgmComIntro(AudioClip audio, AudioClip intro)
        {
            ObterControladorAudio().TocarAudioComIntro(audio, intro);
        }
    }
}
