using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe {
    /// <summary>
    /// Classe responsável por aplicar efeitos visuais de impacto em blocos, como tremor de câmera e partículas.
    /// </summary>
    public class BlockHitEffect : MonoBehaviour
    {
        private ConfiguracaoEfeito configuracaoEfeito;
        private bool estaTremendo = false;
        private Vector3 posicaoOriginalCamera;

        public void IniciarEfeito(ConfiguracaoEfeito _configuracaoEfeito, GameObject particula, Vector3 pontoImpacto)
        {
            configuracaoEfeito = _configuracaoEfeito;
            posicaoOriginalCamera = Camera.main.transform.position;
            Instantiate(particula, pontoImpacto, Quaternion.identity);
            if (!estaTremendo)
                StartCoroutine(CorrotinaTremor());
        }

        private IEnumerator CorrotinaTremor()
        {
            estaTremendo = true;
            float tempoDecorrido = 0f;

            while (tempoDecorrido < configuracaoEfeito.DuracaoTremor)
            {
                tempoDecorrido += Time.deltaTime;
                
                // Cria um tremor aleatório
                float x = Random.Range(-configuracaoEfeito.IntensidadeTremor, configuracaoEfeito.IntensidadeTremor);
                float y = Random.Range(-configuracaoEfeito.IntensidadeTremor, configuracaoEfeito.IntensidadeTremor);
                
                // Aplica o tremor com uma curva de suavização
                float progresso = tempoDecorrido / configuracaoEfeito.DuracaoTremor;
                float suavizacao = 1f - (progresso * progresso);
                
                Vector3 deslocamento = new Vector3(x, y, 0) * suavizacao;
                Camera.main.transform.position = posicaoOriginalCamera + deslocamento;
                
                yield return null;
            }

            // Garante que a câmera volte exatamente para a posição original
            Camera.main.transform.position = posicaoOriginalCamera;
            estaTremendo = false;
        }
    }
}
