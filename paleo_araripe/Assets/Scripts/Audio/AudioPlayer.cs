using System.Collections;
using UnityEngine;

namespace PaleoAraripe
{
    /// <summary>
    /// Classe responsável por controlar a reprodução de áudios (BGM e SFX) no jogo.
    /// Permite tocar, parar, ajustar volume e destruir automaticamente o objeto após o término do áudio.
    /// </summary>
    public class AudioPlayer : MonoBehaviour
    {
        public AudioSource source;
        private bool morrendo = false;
        Coroutine rotinaAjusteVolume;

        void Awake()
        {
            source = GetComponent<AudioSource>();
            source.volume = 0;
            source.playOnAwake = false;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private void ConfigurarSource(AudioClip clip, bool loop, float volume, float tempoAumentarVolume)
        {
            source.volume = volume;
            source.loop = loop;
            source.clip = clip;

            if (tempoAumentarVolume > 0) {
                source.volume = 0;
                rotinaAjusteVolume = StartCoroutine(AjustarVolumeMusica(volume, tempoAumentarVolume));
            }

            if (!source.loop)
                StartCoroutine(DestruirPlayer(clip.length));
        }
        public void ConfigurarVolume(float volume)
        {
            source.volume = volume;
        }
        public AudioSource TocarAudio(AudioClip clip, float volume, float tempoAumentarVolume, bool loop)
        {
            ConfigurarSource(clip, loop, volume, tempoAumentarVolume);
            source.Play();
            return source;
        }

        public AudioSource TocarAudio(AudioClip clip, double tempoSchelude, float volume, float tempoAumentarVolume, bool loop)
        {
            ConfigurarSource(clip, loop, volume, tempoAumentarVolume);
            source.PlayScheduled(tempoSchelude);
            return source;
        }

        public AudioSource TocarSFX(AudioClip clip, float volume)
        {
            ConfigurarSource(clip, false, volume, 0);
            source.Play();
            return source;
        }

        IEnumerator DestruirPlayer(float tempo)
        {
            yield return new WaitForSeconds(tempo);
            Destroy(gameObject);
        }

        public void PararAudio(float volume, float tempo)
        {
            if (morrendo)
                return;

            morrendo = true;
            StopCoroutine(rotinaAjusteVolume);
            StartCoroutine(AjustarVolumeMusica(volume, tempo, true));
        }

        IEnumerator AjustarVolumeMusica(float volume, float tempo, bool destruirAudio = false)
        {
            float tempoPassado = 0f;
            float ajusteVolume = ((volume - source.volume) / tempo) * Time.deltaTime * 2;
                
            while (tempoPassado < tempo)
            {
                source.volume = Mathf.Clamp(source.volume + ajusteVolume, 0, 1);
                tempoPassado += Time.deltaTime;
                yield return null; 
            }
            source.volume = volume;

            if (destruirAudio)
                Destroy(gameObject);
        }
    }
}
