using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe
{
    public class ControladorAudio : MonoBehaviour
    {

        [SerializeField] private GameObject prefabAudioPlayer;
        private List<AudioPlayer> audioPlayers = new List<AudioPlayer>();

        private const float TEMPO_AUMENTAR_VOLUME = 2f;
        private const float TEMPO_DIMINUIR_VOLUME = 1f;
        private const float VOLUME_MAXIMO = 0.5f;
        private const float VOLUME_MINIMO = 0;

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void PararTodosAudios()
        {
            foreach (AudioPlayer player in audioPlayers)
            {
                if(player != null)
                    player.PararAudio(VOLUME_MINIMO, TEMPO_DIMINUIR_VOLUME);
            }
            audioPlayers.Clear();
        }

        public void PararTodosAudios(bool realmenteParar)
        {
            if(realmenteParar)
                PararTodosAudios();
        }

        private AudioPlayer InstanciarAudioPlayer()
        {
            AudioPlayer audioPlayer = Instantiate(prefabAudioPlayer, transform).GetComponent<AudioPlayer>();
            audioPlayers.Add(audioPlayer);

            return audioPlayer;
        }

        public void TocarAudioComIntro(AudioClip clip, AudioClip intro, float volumeMaximo = VOLUME_MAXIMO, float tempo = TEMPO_AUMENTAR_VOLUME,  bool pararOutrosAudios = true)
        {
            PararTodosAudios(pararOutrosAudios);

            double tempoSchedule = AudioSettings.dspTime + 1f;
            InstanciarAudioPlayer().TocarAudio(intro, tempoSchedule, volumeMaximo, tempo, false);

            tempoSchedule += (double) intro.samples / intro.frequency;
            InstanciarAudioPlayer().TocarAudio(clip, tempoSchedule, volumeMaximo, tempo, true);
        }

        public void TocarAudio(AudioClip clip, float volumeMaximo = VOLUME_MAXIMO, float tempo = TEMPO_AUMENTAR_VOLUME, bool pararOutrosAudios = true)
        {
            PararTodosAudios(pararOutrosAudios);
            InstanciarAudioPlayer().TocarAudio(clip, volumeMaximo, tempo, true);
        }

    }
}
