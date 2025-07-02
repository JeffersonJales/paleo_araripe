using UnityEngine;

namespace PaleoAraripe
{
    /// <summary>
    /// Classe utilitária estática para facilitar o acesso e controle global do áudio no jogo.
    /// </summary>
    public class UtilitarioAudio : MonoBehaviour
    {
        public static ControladorAudio controladorAudio = null;

        public static ControladorAudio ObterControladorAudio()
        {
            if (controladorAudio == null)
                controladorAudio = FindObjectOfType<ControladorAudio>();

            return controladorAudio;
        }
        
        public static void TocarBGM(AudioClip audio, float volume = 1)
        {
            ObterControladorAudio().TocarAudio(audio, volume);
        }

        public static void TocarBgmComIntro(AudioClip audio, AudioClip intro, float volume = 1)
        {
            ObterControladorAudio().TocarAudioComIntro(audio, intro, volume);
        }

        public static void TocarSFX(AudioClip audio, float volume = 1)
        {
            ObterControladorAudio().TocarSfx(audio, volume);
        }
    
        public static void SetarVolumeMusica(float volume)
        {
            ObterControladorAudio().SetarVolumeMusica(volume);
        }

        public static void SetarVolumeSFX(float volume)
        {
            ObterControladorAudio().SetarVolumeSFX(volume);
        }
    }
}
