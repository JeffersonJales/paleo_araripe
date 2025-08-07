using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe
{
    public class UIFerramentas : MonoBehaviour
    {
        private Animator anim;
        private bool estaAberto;
        public void Awake()
        {
            anim = GetComponent<Animator>();
            estaAberto = false;
        }
        public void MudarEstadoBotaoFerramenta()
        {
            estaAberto = !estaAberto;
            anim.SetBool("aberto", estaAberto);
        }
    }
}
