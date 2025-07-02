using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe
{
    /// <summary>
    /// Classe responsável por destruir automaticamente o GameObject após o término do efeito de partícula.
    /// </summary>
    public class DestruirParticul : MonoBehaviour
    {
        void Start()
        {
            Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
        }
    }
}
