using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaleoAraripe
{
    public class BotaoSwitchComportamento : MonoBehaviour
    {
        [SerializeField] private string textoSwitchLigado;
        [SerializeField] private string textoSwitchDesligado;
        [SerializeField] private Button botaoSwitch;
        [SerializeField] private TMP_Text textoResultado;
        [SerializeField] private Animator animator;
        [SerializeField] private UnityEvent eventosBotaoTroca;
        private bool estadoSwitch;
        public bool EstadoSwitch { get { return estadoSwitch; } }
        public void ConfigurarBotaoSwitch(bool estado)
        {
            estadoSwitch = estado;
            animator.SetBool("estadoSwitch", estadoSwitch);
            textoResultado.SetText(estadoSwitch ? textoSwitchLigado : textoSwitchDesligado);
        }
        public void Switch(){
            estadoSwitch = !estadoSwitch;
            animator.SetBool("estadoSwitch", estadoSwitch);
            textoResultado.SetText(estadoSwitch ? textoSwitchLigado : textoSwitchDesligado);
            eventosBotaoTroca.Invoke();
        }
        private void OnEnable()
        {
            animator.SetBool("estadoSwitch", estadoSwitch);
            textoResultado.SetText(estadoSwitch ? textoSwitchLigado : textoSwitchDesligado);
        }
    }
}
