using System;
using System.Collections.Generic;
using UnityEngine;

public class UsarFerramentas : MonoBehaviour
{
    [SerializeField] private List<TrocarFerramentaViaBotaoUI> listaDeItensUI = new List<TrocarFerramentaViaBotaoUI>();
    [SerializeField] private FerramentaSO ferramentaEquipada;
    [SerializeField] private GameObject blocoAlvoRaycast;
    private List<GameObject> alvosFerramenta = new List<GameObject>();

    private Camera cam;
    public event Action<FerramentaSO> EventoUsarFerramenta;
    public event Action<ResumoInteracaoBlocoFerramenta> EventoResumoInteracao;


    public void Start()
    {
        cam = Camera.main;

        foreach(var item in listaDeItensUI)
        {
            item.aoPressionarBotao += trocarFerramenta;
            EventoUsarFerramenta += item.aoUtilizarFerramenta;
        }
    }

    public void FixedUpdate()
    {
        procurarBlocoAreia();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            usarFerramenta();
    }

    #region Raycast

    private void procurarBlocoAreia()
    {
        if (ferramentaEquipada == null)
            return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objetoAtingido = hit.collider.gameObject;

            if (objetoAtingido.Equals(blocoAlvoRaycast) || !objetoAtingido.activeInHierarchy) 
                return;

            procurarBlocosAlvoFerramenta(objetoAtingido);
        }
        else if(blocoAlvoRaycast != null)
        {
            limparAlvos();
        }
    }

    public void procurarBlocosAlvoFerramenta(GameObject alvoAtual)
    {
        /// Desativar alvos antigos
        if (blocoAlvoRaycast != null)
            blocoAlvoRaycast.GetComponent<BlocoGenerico>().casoDeixeDeSerFocoDaFerramenta();

        foreach (GameObject bloco in alvosFerramenta)
        {
            bloco.GetComponent<BlocoGenerico>().casoDeixeDeSerFocoDaFerramenta();
        }
        alvosFerramenta.Clear();
        
        /// Atualizar alvo
        blocoAlvoRaycast = alvoAtual;
        alvoAtual.GetComponent<BlocoGenerico>().casoSejaFocoDaFerramenta();

        Vector3 area = ferramentaEquipada.AreaEfeito;
        if (area != Vector3.one) {

            Collider[] colliders = Physics.OverlapBox(blocoAlvoRaycast.transform.position, area / 2);
            foreach(Collider collider in colliders)
            {
                if(!collider.gameObject.Equals(blocoAlvoRaycast)) { 
                    alvosFerramenta.Add(collider.gameObject);
                    collider.gameObject.GetComponent<BlocoGenerico>().casoSejaFocoDaFerramenta();
                }
            }
        }
    }

    public void usarFerramenta()
    {
        if (ferramentaEquipada == null || blocoAlvoRaycast == null) 
            return;

        ResumoInteracaoBlocoFerramenta resumo;
        InteracaoBlocoFerramenta interacao = new InteracaoBlocoFerramenta();

        interacao.interacaoFerramentaComBloco(ferramentaEquipada, blocoAlvoRaycast.GetComponent<BlocoGenerico>(), out resumo);

        if(resumo.BlocoDestruido)
            blocoAlvoRaycast = null;

        for(var i  = 0; i < alvosFerramenta.Count; i++)
        {
            ResumoInteracaoBlocoFerramenta resumoBlocos;
            interacao.interacaoFerramentaComBloco(ferramentaEquipada, alvosFerramenta[i].GetComponent<BlocoGenerico>(), out resumoBlocos);
            if (resumoBlocos.BlocoDestruido)
                alvosFerramenta.RemoveAt(i--);
        }
        
        limparAlvos();
        EventoUsarFerramenta?.Invoke(ferramentaEquipada);
        EventoResumoInteracao?.Invoke(resumo);
    }

    private void limparAlvos()
    {
        if (blocoAlvoRaycast != null)
            blocoAlvoRaycast.GetComponent<BlocoGenerico>().casoDeixeDeSerFocoDaFerramenta();

        blocoAlvoRaycast = null;
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
        limparAlvos();
        procurarBlocoAreia();
    }

    #endregion
}

