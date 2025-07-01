using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace PaleoAraripe
{
    public class ControladorAudio : MonoBehaviour
    {

        [Range(0f, 1f)][SerializeField] private float volumeBGM = 1;
        [Range(0f, 1f)][SerializeField] private float volumeSFX = 1;


        [SerializeField] private GameObject prefabAudioPlayer;
        [SerializeField] private AudioMixerGroup audioMixerBGM;
        [SerializeField] private AudioMixerGroup audioMixerSFX;

        private List<AudioPlayer> audioPlayers = new List<AudioPlayer>();

        private const float TEMPO_AUMENTAR_VOLUME = 2f;
        private const float TEMPO_DIMINUIR_VOLUME = 1f;
        public float VolumeBGM { set { volumeBGM = value; } }
        public float VolumeSFX { set { volumeSFX = value; } }


        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void PararTodosAudios()
        {
            foreach (AudioPlayer player in audioPlayers)
            {
                if(player != null)
                    player.PararAudio(0, TEMPO_DIMINUIR_VOLUME);
            }
            audioPlayers.Clear();
        }

        private AudioPlayer InstanciarAudioPlayer(AudioMixerGroup mixer, bool adicionarPlayer = true)
        {
            AudioPlayer audioPlayer = Instantiate(prefabAudioPlayer, transform).GetComponent<AudioPlayer>();
            audioPlayer.source.outputAudioMixerGroup = mixer;
            
            if(adicionarPlayer)
                audioPlayers.Add(audioPlayer);
            
            return audioPlayer;
        }

        public void TocarAudioComIntro(AudioClip clip, AudioClip intro, float volume = 1, float tempo = TEMPO_AUMENTAR_VOLUME)
        {
            PararTodosAudios();

            double tempoSchedule = AudioSettings.dspTime + 1f;
            InstanciarAudioPlayer(audioMixerBGM).TocarAudio(intro, tempoSchedule, volume * volumeBGM, tempo, false);

            tempoSchedule += (double) intro.samples / intro.frequency;
            InstanciarAudioPlayer(audioMixerBGM).TocarAudio(clip, tempoSchedule, volume * volumeBGM, tempo, true);
        }

        public void TocarAudio(AudioClip clip, float volumeMaximo = 1, float tempo = TEMPO_AUMENTAR_VOLUME)
        {
            PararTodosAudios();
            InstanciarAudioPlayer(audioMixerBGM).TocarAudio(clip, volumeMaximo, tempo, true);
        }

        public void TocarSfx(AudioClip clip, float volume = 1)
        {
            InstanciarAudioPlayer(audioMixerSFX, false).TocarSFX(clip, volume * volumeSFX);
        }
    }
}
