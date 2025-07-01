using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PaleoAraripe {
    public class ControladorFerramenta : Singleton<ControladorFerramenta> {

        [SerializeField] private Boolean ativado = true;
        [SerializeField] private FerramentaSO ferramentaEquipada;
        [SerializeField] private FerramentaSO ferramentaDesequipada;
        [SerializeField] private GameObject blocoAlvoRaycast;
        [Range(15f, 100f)][SerializeField] private float distanciaMaximaColisaoRaycast = 1000f;
        
        [SerializeField] private int inspiracaoAtual = 0;
        [Range(5, 50)][SerializeField] private int inspiracaoMaxima = 50;
        
        [SerializeField] private BlackBoardInformacoesPartida bbInformacoesPartida;
        [SerializeField] private BlockHitEffect blockHitEffect;
        private Camera cam;
        private Vector3 normalRaycast;
        private LayerMask mascaraColisaoBloco;

        private List<GameObject> alvosFerramenta = new List<GameObject>();
        public event Action<ResumoInteracaoBlocoFerramenta> EventoAposRealizarUsoFerramenta;

        [SerializeField] public int TURNOS_FERRAMENTA_CONGELADA = 2;

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
            ConfigurarListeners();
        }

        public void OnDestroy()
        {
            DesconfigurarListeners();
        }

        private void ConfigurarListeners()
        {
            var controleFim = FindObjectOfType<ControladorEstadoPartida>();
            if (controleFim != null)
                controleFim.AoFinalizarPartida += AoFinalizarPartida;
        }

        private void DesconfigurarListeners()
        {
            var controleFim = FindObjectOfType<ControladorEstadoPartida>();
            if (controleFim != null)
                controleFim.AoFinalizarPartida -= AoFinalizarPartida;
        }


        public void FixedUpdate()
        {
            ProcurarBlocoAlvoRaycast();
        }

        public void Update()
        {
            if (UtilitariosInput.InteracaoUsarFerramenta() && ativado)
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
            ResumoInteracaoBlocoFerramenta resumo = new InteracaoBlocoFerramenta().interacaoFerramentaComBloco(ferramentaEquipada, blocosGenericos, true);

            /// Ganho de Inspiracao
            inspiracaoAtual = Math.Clamp(inspiracaoAtual + ferramentaEquipada.Inspiracao + resumo.QuantidadeInspiracaoGanha, 0, inspiracaoMaxima);
            bbInformacoesPartida.SetValue(bbInformacoesPartida.INSPIRACAO_ATUAL, inspiracaoAtual);
            bbInformacoesPartida.SetValue(bbInformacoesPartida.INSPIRACAO_MAXIMA, inspiracaoMaxima);

            foreach(var alvo in blocosGenericos)
            {
                if (alvo.estaVivo())
                    alvo.casoDeixeDeSerFocoDaFerramenta();
            }

            blocoAlvoRaycast = null;
            alvosFerramenta.Clear();

            EventoAposRealizarUsoFerramenta?.Invoke(resumo);
        }

        private void ProcurarBlocoAlvoRaycast()
        {
            if (ferramentaEquipada == null)
                return;

            Ray ray = UtilitariosInput.ObterRaioProcurarBloco(cam);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distanciaMaximaColisaoRaycast, mascaraColisaoBloco))
            {
                GameObject objetoAtingido = hit.collider.transform.parent.gameObject;
                objetoAtingido.GetComponent<BlocoGenerico>().SetPontoImpacto(hit.point);
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

        private void AoFinalizarPartida()
        {
            ativado = false;
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
            if (ferramenta == null) 
                ferramenta = ferramentaDesequipada;
            
            ferramentaEquipada = ferramenta;

            desativarFocoAlvos();
            ProcurarBlocoAlvoRaycast();
        }

        #endregion

    }
}