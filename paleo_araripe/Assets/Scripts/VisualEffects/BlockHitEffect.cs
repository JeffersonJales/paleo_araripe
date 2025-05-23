using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHitEffect : MonoBehaviour
{
    [SerializeField] private GameObject particulaImpacto;
    [SerializeField] private ConfiguracaoEfeito configuracaoEfeito;

    private ParticleSystem impactoParticulaComponente;
    private bool estaTremendo = false;
    private bool podeClicar = true;
    private Vector3 posicaoOriginalCamera;

    public void ConfigurarEfeito(ConfiguracaoEfeito novaConfiguracao)
    {
        configuracaoEfeito = novaConfiguracao;
    }

    private void Start()
    {
        impactoParticulaComponente = particulaImpacto.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && podeClicar)
        {
            DetectarClique();
        }
    }

    private void DetectarClique()
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(raio, out hit, Mathf.Infinity, configuracaoEfeito.LayerMask))
        {
            posicaoOriginalCamera = Camera.main.transform.position;
            Vector3 pontoImpacto = hit.point;
            StartCoroutine(CicloDeVidaParticula(pontoImpacto));
            if (!estaTremendo)
                StartCoroutine(CorrotinaTremor());
            StartCoroutine(CooldownCoroutine());
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        podeClicar = false;
        yield return new WaitForSeconds(configuracaoEfeito.Cooldown);
        podeClicar = true;
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

    private IEnumerator CicloDeVidaParticula(Vector3 pontoImpacto)
    {
        if (particulaImpacto != null)
        {
            particulaImpacto.transform.position = pontoImpacto;
            particulaImpacto.SetActive(true);
            yield return new WaitForSeconds(impactoParticulaComponente.main.duration);
            particulaImpacto.SetActive(false);
        }
    }
}
