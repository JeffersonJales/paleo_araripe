using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe
{
    public class DesabilitarFossilQuebradoUI : MonoBehaviour
    {
        [SerializeField] private AudioClip sfx;
        private void OnEnable()
        {
            UtilitarioAudio.TocarSFX(sfx);
        }
        public void DisableUIFeedback()
        {
            gameObject.SetActive(false);
        }
    }
}
