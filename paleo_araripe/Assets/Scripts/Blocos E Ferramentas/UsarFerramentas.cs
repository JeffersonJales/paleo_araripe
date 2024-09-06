using System;
using System.Collections.Generic;
using UnityEngine;

public class UsarFerramentas : MonoBehaviour
{
    [SerializeField] private List<TrocarFerramentaViaBotaoUI> listaDeItensUI = new List<TrocarFerramentaViaBotaoUI>();
    [SerializeField] private FerramentaSO ferramentaEquipada;
    [SerializeField] private GameObject blocoAtual;
    private List<GameObject> outrosAlvos = new List<GameObject>();

    private Camera cam;

    public event Action<FerramentaSO> EventoUsarFerramenta;


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

            if (objetoAtingido.Equals(blocoAtual) || !objetoAtingido.activeInHierarchy) 
                return;

            procurarBlocosAlvoFerramenta(objetoAtingido);
        }
        else if(blocoAtual != null)
        {
            limparAlvos();
        }
    }

    public void procurarBlocosAlvoFerramenta(GameObject alvoAtual)
    {
        /// Desativar alvos antigos
        if (blocoAtual != null)
            blocoAtual.GetComponent<BlocoGenerico>().casoDeixeDeSerFocoDaFerramenta();

        foreach (GameObject bloco in outrosAlvos)
        {
            bloco.GetComponent<BlocoGenerico>().casoDeixeDeSerFocoDaFerramenta();
        }
        outrosAlvos.Clear();
        
        /// Atualizar alvo
        blocoAtual = alvoAtual;
        alvoAtual.GetComponent<BlocoGenerico>().casoSejaFocoDaFerramenta();

        Vector3 area = ferramentaEquipada.AreaEfeito;
        if (area != Vector3.one) {

            Collider[] colliders = Physics.OverlapBox(blocoAtual.transform.position, area / 2);
            foreach(Collider collider in colliders)
            {
                if(!collider.gameObject.Equals(blocoAtual)) { 
                    outrosAlvos.Add(collider.gameObject);
                    collider.gameObject.GetComponent<BlocoGenerico>().casoSejaFocoDaFerramenta();
                }
            }
        }
    }

    public void usarFerramenta()
    {
        if (ferramentaEquipada == null || blocoAtual == null) 
            return;

        if (blocoAtual.GetComponent<BlocoGenerico>().interagir())
            blocoAtual = null;

        for(var i  = 0; i < outrosAlvos.Count; i++)
        {
            if (outrosAlvos[i].GetComponent<BlocoGenerico>().interagir())
                outrosAlvos.RemoveAt(i--);
        }
        
        limparAlvos();
        EventoUsarFerramenta?.Invoke(ferramentaEquipada);
    }

    private void limparAlvos()
    {
        if (blocoAtual != null)
            blocoAtual.GetComponent<BlocoGenerico>().casoDeixeDeSerFocoDaFerramenta();

        blocoAtual = null;
        foreach (GameObject bloco in outrosAlvos)
        {
            bloco.GetComponent<BlocoGenerico>().casoDeixeDeSerFocoDaFerramenta();
        }
        outrosAlvos.Clear();
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

