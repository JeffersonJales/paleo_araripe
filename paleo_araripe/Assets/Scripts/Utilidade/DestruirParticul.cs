using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe
{
    public class DestruirParticul : MonoBehaviour
    {
        void Start()
        {
            Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
        }
    }
}
