using System;
using System.Collections.Generic;
using UnityEngine;
using Utilidades;

public class UsarFerramentas : MonoBehaviour
{
    [SerializeField] private FerramentaSO ferramentaEquipada;
    [SerializeField] private GameObject blocoAlvoRaycast;
    [Range(15f, 30f)][SerializeField] private float distanciaMaximaColisaoRaycast = 20f;
    
    [SerializeField] private int inspiracaoAtual = 0;
    [Range(5, 50)][SerializeField] private int inspiracaoMaxima = 50;
    
    [SerializeField] private BlackBoardSO bbInformacoesPartida;

    private Camera cam;
    private Vector3 normalRaycast;
    private LayerMask mascaraColisaoBloco;

    private List<GameObject> alvosFerramenta = new List<GameObject>();
    public event Action<ResumoInteracaoBlocoFerramenta> EventoAposRealizarUsoFerramenta;

    // Inspector
    public void OnValidate()
    {
        inspiracaoAtual = Mathf.Clamp(inspiracaoAtual, 0, InspiracaoMaxima);
    }

    // Getters / Setters 
    public int InspiracaoAtual => inspiracaoAtual;
    public int InspiracaoMaxima => inspiracaoMaxima;


    public void Start()
    {
        cam = Camera.main;
        mascaraColisaoBloco = new ColisoesBlocosChao().obterMascaraBlocoArqueologico();
    }

    public void FixedUpdate()
    {
        procurarBlocoAlvoRaycast();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            utilizarFerramentaEquipada();
    }

    
    public void utilizarFerramentaEquipada()
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
        bbInformacoesPartida.SetValue(BBChaveTuplaInfomacoesPartida.INSPIRACAO_ATUAL, inspiracaoAtual);
        bbInformacoesPartida.SetValue(BBChaveTuplaInfomacoesPartida.INSPIRACAO_MAXIMA, inspiracaoMaxima);

        /// Desativar os blocos que sobreviveram
        for (var i = 0; i < resumo.TipoInteracaoBloco.Count; i++)
        {
            if (!NaturezaBlocoFerramenta.interacaoPodeResultarNaDestruicaoDoBloco(resumo.TipoInteracaoBloco[i]))
                blocosGenericos[i].casoDeixeDeSerFocoDaFerramenta();
        }

        blocoAlvoRaycast = null;
        alvosFerramenta.Clear();

        EventoAposRealizarUsoFerramenta?.Invoke(resumo);
    }

    private void procurarBlocoAlvoRaycast()
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

            procurarAlvos(objetoAtingido, normal);
        }
        else if(blocoAlvoRaycast != null)
        {
            blocoAlvoRaycast = null;
            desativarFocoAlvos();
        }
    }

    
    #region Feedback Visual Blocos Marcados
    public void procurarAlvos(GameObject alvoAtual, Vector3 normal)
    {
        normalRaycast = normal;
        blocoAlvoRaycast = alvoAtual;
        desativarFocoAlvos();

        alvosFerramenta = NaturezaBlocoFerramenta.obterListaBlocosPorFerramenta(ferramentaEquipada, blocoAlvoRaycast, normal);
        foreach (var item in alvosFerramenta)
        {
            item.GetComponent<BlocoGenerico>().casoSejaFocoDaFerramenta();
        }
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
    public void trocarFerramentaEquipada(FerramentaSO ferramenta)
    {
        ferramentaEquipada = ferramenta;
        desativarFocoAlvos();
        procurarBlocoAlvoRaycast();
    }

    #endregion

}