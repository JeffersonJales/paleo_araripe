using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlasticPipe.PlasticProtocol.Messages.Serialization.ItemHandlerMessagesSerialization;

public class UtilizarFerramenta : MonoBehaviour
{
    
    [SerializeField] private FerramentaSO[] ferramentas = new FerramentaSO[3];
    [SerializeField] private FerramentaSO ferramentaEquipada;
    [SerializeField] private GameObject blocoAtual;
    private List<GameObject> outrosAlvos = new List<GameObject>();

    private Camera cam;

    public event Action<FerramentaSO> EventoUsarFerramenta;


    public void Start()
    {
        cam = Camera.main;
        trocarParaFerramenta0();
    }

    public void FixedUpdate()
    {
        procurarBlocoAreia();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            usarFerramenta();

        trocarFerramentaApertandoBotoes();
    }

    #region Raycast

    private void procurarBlocoAreia()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objetoAtingido = hit.collider.gameObject;

            if (objetoAtingido.Equals(blocoAtual) || !objetoAtingido.activeInHierarchy) 
                return;

            procurarBlocosAlvoFerramenta(objetoAtingido);
        }
    }

    public void procurarBlocosAlvoFerramenta(GameObject alvoAtual)
    {
        /// Desativar alvos antigos
        if (blocoAtual != null)
            blocoAtual.GetComponent<BlocoAreia>().casoDeixeDeSerFocoDaFerramenta();

        foreach (GameObject bloco in outrosAlvos)
        {
            bloco.GetComponent<BlocoAreia>().casoDeixeDeSerFocoDaFerramenta();
        }
        outrosAlvos.Clear();
        
        /// Atualizar alvo
        blocoAtual = alvoAtual;
        alvoAtual.GetComponent<BlocoAreia>().casoSejaFocoDaFerramenta();

        Vector3 area = ferramentaEquipada.AreaEfeito;
        if (area != Vector3.one) {

            Collider[] colliders = Physics.OverlapBox(blocoAtual.transform.position, area / 2);
            foreach(Collider collider in colliders)
            {
                if(!collider.gameObject.Equals(blocoAtual)) { 
                    outrosAlvos.Add(collider.gameObject);
                    collider.gameObject.GetComponent<BlocoAreia>().casoSejaFocoDaFerramenta();
                }
            }
        }
    }

    public void usarFerramenta()
    {
        if (ferramentaEquipada == null || blocoAtual == null) 
            return;

        if (blocoAtual.GetComponent<BlocoAreia>().interagir())
            blocoAtual = null;

        for(var i  = 0; i < outrosAlvos.Count; i++)
        {
            if (outrosAlvos[i].GetComponent<BlocoAreia>().interagir())
                outrosAlvos.RemoveAt(i--);
        }
        
        limparAlvos();
        EventoUsarFerramenta?.Invoke(ferramentaEquipada);
    }

    private void limparAlvos()
    {
        if (blocoAtual != null)
            blocoAtual.GetComponent<BlocoAreia>().casoDeixeDeSerFocoDaFerramenta();

        blocoAtual = null;
        foreach (GameObject bloco in outrosAlvos)
        {
            bloco.GetComponent<BlocoAreia>().casoDeixeDeSerFocoDaFerramenta();
        }
        outrosAlvos.Clear();
    }

    #endregion


    #region Troca de ferramentas!

    private void trocarFerramentaApertandoBotoes()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            trocarParaFerramenta0();
        else if (Input.GetKeyDown(KeyCode.W))
            trocarParaFerramenta1();
        else if (Input.GetKeyDown(KeyCode.E))
            trocarParaFerramenta2();
    }

    private void trocarFerramenta(FerramentaSO ferramenta)
    {
        ferramentaEquipada = ferramenta;
        limparAlvos();
        procurarBlocoAreia();
    }

    public void trocarParaFerramenta0() { trocarFerramenta(ferramentas[0]); }
    
    public void trocarParaFerramenta1() { trocarFerramenta(ferramentas[1]); }

    public void trocarParaFerramenta2() { trocarFerramenta(ferramentas[2]); }

    #endregion
}

