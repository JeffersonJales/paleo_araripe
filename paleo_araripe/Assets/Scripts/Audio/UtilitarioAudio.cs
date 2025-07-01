using UnityEngine;

namespace PaleoAraripe
{
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
            ObterControladorAudio().VolumeBGM = volume;
        }

        public static void SetarVolumeSFX(float volume)
        {
            ObterControladorAudio().VolumeSFX = volume;
        }
    }
}
