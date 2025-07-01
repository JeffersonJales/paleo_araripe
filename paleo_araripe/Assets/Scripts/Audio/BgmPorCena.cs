using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaleoAraripe
{
    public class BgmPorCena : MonoBehaviour
    {
        public AudioClip menu;
        public AudioClip menuIntro;

        public AudioClip fase;
        public AudioClip faseIntro;

        void Start()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void OnSceneLoaded(Scene cena, LoadSceneMode modo)
        {
            TocarMusica(cena.name);
        }

        public void TocarMusica(string nomeCena)
        {
            switch (nomeCena)
            {
                case "MenuPrincipal": UtilitarioAudio.TocarBgmComIntro(menu, menuIntro); break;
                case "CenaPrincipal": UtilitarioAudio.TocarBgmComIntro(fase, faseIntro); break;
            }
        }
    }
}
