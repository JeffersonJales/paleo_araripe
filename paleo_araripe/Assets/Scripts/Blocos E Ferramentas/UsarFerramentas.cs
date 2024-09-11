using System;
using System.Collections.Generic;
using UnityEngine;

public class UsarFerramentas : MonoBehaviour
{
    [SerializeField] private FerramentaSO ferramentaEquipada;
    [SerializeField] private GameObject blocoAlvoRaycast;
    [SerializeField] private LayerMask mascaraColisaoBloco;

    [Range(15f, 30f)]
    [SerializeField] private float distanciaMaximaColisaoRaycast = 20f;


    [Range(5, 30)]
    [SerializeField] private int inspiracaoMaxima = 30;

    private int inspiracaoAtual = 0;

    private Camera cam;
    private List<GameObject> alvosFerramenta = new List<GameObject>();

    public event Action<ResumoInteracaoBlocoFerramenta> EventoResumoInteracao;


    public void Start()
    {
        cam = Camera.main;

        foreach (var item in FindObjectsOfType<TrocarFerramentaViaBotaoUI>())
        {
            item.aoPressionarBotao += trocarFerramenta;
            EventoResumoInteracao += item.aoUtilizarFerramenta;
        }
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

            if (objetoAtingido.Equals(blocoAlvoRaycast) || !objetoAtingido.activeInHierarchy) 
                return;

            procurarBlocosAlvoFerramenta(objetoAtingido);
        }
        else if(blocoAlvoRaycast != null)
        {
            blocoAlvoRaycast = null;
            desativarFocoAlvos();
        }
    }

    public void procurarBlocosAlvoFerramenta(GameObject alvoAtual)
    {
        blocoAlvoRaycast = alvoAtual;
        desativarFocoAlvos();

        Vector3 area = ferramentaEquipada.AreaEfeito;
        Collider[] colliders = Physics.OverlapBox(blocoAlvoRaycast.transform.position, area / 2);
        foreach(Collider collider in colliders)
        {
            alvosFerramenta.Add(collider.gameObject);
            collider.gameObject.GetComponent<BlocoGenerico>().casoSejaFocoDaFerramenta();
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

        /// Desativar os blocos que sobreviveram
        for(var i = 0; i < resumo.TipoInteracaoBloco.Count; i++)
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
    private void trocarFerramenta(FerramentaSO ferramenta)
    {
        ferramentaEquipada = ferramenta;
        desativarFocoAlvos();
        procurarBlocoRaycast();
    }

    #endregion
}

