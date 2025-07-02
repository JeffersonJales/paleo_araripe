using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PaleoAraripe
{
    /// <summary>
    /// Classe responsável por alterar dinamicamente o tamanho da fonte de textos na interface, conforme configuração do usuário.
    /// </summary>
    public class SetarTamanhoFonte : MonoBehaviour
    {
        public static Action<bool> mudarTamanhoFonte;
        private TMP_Text texto;
        private float fonteOriginal;

        private void Awake()
        {
            texto = GetComponent<TMP_Text>();
            fonteOriginal = texto.fontSize;
        }
        private void OnEnable()
        {
            mudarTamanhoFonte += MudarTamanhoFonte;
        }
        private void OnDisable()
        {
            mudarTamanhoFonte -= MudarTamanhoFonte;
        }
        private void MudarTamanhoFonte(bool pequeno)
        {
            if(pequeno)
                texto.fontSize = fonteOriginal;
            else
                texto.fontSize = fonteOriginal + fonteOriginal / 2;
        }
    }
}
