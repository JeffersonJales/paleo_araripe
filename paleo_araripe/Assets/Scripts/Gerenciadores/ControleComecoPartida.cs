using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe
{
    public class ControleComecoPartida : MonoBehaviour
    {
        [SerializeField] private GameObject[] estagios;
        private void Start()
        {
            CriarEstagio();
        }
        public void CriarEstagio()
        {
            int levelParaCarregar = GerenciadorDados.Instance.levelSelecionado;
            GameObject.Instantiate(estagios[levelParaCarregar]);
        }
    }
}
