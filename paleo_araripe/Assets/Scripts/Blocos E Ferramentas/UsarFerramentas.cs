using System;
using System.Collections.Generic;
using UnityEngine;

public class UsarFerramentas : MonoBehaviour
{
    [SerializeField] private FerramentaSO ferramentaEquipada;
    [SerializeField] private GameObject blocoAlvoRaycast;
    [SerializeField] private LayerMask mascaraColisaoBloco;
    [SerializeField] private QuadroNegroSO infoJogo;

    [Range(15f, 30f)]
    [SerializeField] private float distanciaMaximaColisaoRaycast = 20f;


    [Range(5, 30)][SerializeField] private int inspiracaoMaxima = 30;
    public int InspiracaoMaxima => inspiracaoMaxima;
    [SerializeField] private Vector3 normalRaycast;

    [SerializeField] private int inspiracaoAtual = 0;
    public int InspiracaoAtual => inspiracaoAtual;


    private Camera cam;
    private List<GameObject> alvosFerramenta = new List<GameObject>();

    public event Action<ResumoInteracaoBlocoFerramenta> EventoResumoInteracao;


    public void OnValidate()
    {
        inspiracaoAtual = Mathf.Clamp(inspiracaoAtual, 0, InspiracaoMaxima);
    }

    public void Start()
    {
        cam = Camera.main;
    }

    public void FixedUpdate()
    {
        procurarBlocoRaycast();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            usarFerramenta();
    }

    #region Raycast

    private void procurarBlocoRaycast()
    {
        if (ferramentaEquipada == null)
            return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanciaMaximaColisaoRaycast, mascaraColisaoBloco))
        {
            GameObject objetoAtingido = hit.collider.gameObject;
            Vector3 normal = hit.normal;

            if ((objetoAtingido.Equals(blocoAlvoRaycast) && normal.Equals(normalRaycast)) || !objetoAtingido.activeInHierarchy) 
                return;

            procurarBlocosAlvoFerramenta(objetoAtingido, normal);
        }
        else if(blocoAlvoRaycast != null)
        {
            blocoAlvoRaycast = null;
            desativarFocoAlvos();
        }
    }

    public void procurarBlocosAlvoFerramenta(GameObject alvoAtual, Vector3 normal)
    {
        normalRaycast = normal;
        blocoAlvoRaycast = alvoAtual;
        desativarFocoAlvos();

        alvosFerramenta = NaturezaBlocoFerramenta.obterListaBlocosPorFerramenta(ferramentaEquipada, blocoAlvoRaycast, normal);
        foreach(var item in alvosFerramenta)
        {
            item.GetComponent<BlocoGenerico>().casoSejaFocoDaFerramenta();
        }
    }

    public void usarFerramenta()
    {
        if (ferramentaEquipada == null || blocoAlvoRaycast == null) 
            return;

        /// Resgatar todos os scripts dos alvos
        List<BlocoGenerico> blocosGenericos = new List<BlocoGenerico>();
        foreach (var alvo in alvosFerramenta)
        {
            blocosGenericos.Add(alvo.GetComponent<BlocoGenerico>());
        }

        /// Realizar interação entre blocos e ferramentas
        ResumoInteracaoBlocoFerramenta resumo = new InteracaoBlocoFerramenta().interacaoFerramentaComBloco(ferramentaEquipada, blocosGenericos);

        /// Inspiração
        inspiracaoAtual = Math.Clamp(inspiracaoAtual + ferramentaEquipada.Inspiracao, 0, inspiracaoMaxima);
        infoJogo.SetValue(QuadroNegroInfoJogoChaves.INSPIRACAO_ATUAL, inspiracaoAtual);
        infoJogo.SetValue(QuadroNegroInfoJogoChaves.INSPIRACAO_MAXIMA, inspiracaoMaxima);

        /// Desativar os blocos que sobreviveram
        for (var i = 0; i < resumo.TipoInteracaoBloco.Count; i++)
        {
            if (!NaturezaBlocoFerramenta.interacaoPodeResultarNaDestruicaoDoBloco(resumo.TipoInteracaoBloco[i]))
                blocosGenericos[i].casoDeixeDeSerFocoDaFerramenta();
        }
        
        blocoAlvoRaycast = null;
        alvosFerramenta.Clear();

        EventoResumoInteracao?.Invoke(resumo);
    }

    private void desativarFocoAlvos()
    {
        foreach (GameObject bloco in alvosFerramenta)
        {
            bloco.GetComponent<BlocoGenerico>().casoDeixeDeSerFocoDaFerramenta();
        }
        alvosFerramenta.Clear();
    }

    #endregion


    #region Troca de ferramentas!
    public void trocarFerramenta(FerramentaSO ferramenta)
    {
        ferramentaEquipada = ferramenta;
        desativarFocoAlvos();
        procurarBlocoRaycast();
    }

    #endregion
}

