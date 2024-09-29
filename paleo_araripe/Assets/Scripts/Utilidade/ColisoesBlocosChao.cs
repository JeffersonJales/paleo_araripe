using log4net.Util;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Utilidades
{
    public class ColisoesBlocosChao 
    {
        public const string NOME_LAYER_BLOCO_ARQUEOLOGICO = "BlocoArqueologico";
        public const string NOME_LAYER_CHAO = "Chao";

        private const int MASCARA_NAO_ENCONTRADA = -1;
        private int mascaraBlocoArqueologico = MASCARA_NAO_ENCONTRADA;
        private int mascaraChao = MASCARA_NAO_ENCONTRADA;
        private float tamanhoMinimoRaio = 20f;

        #region Máscaras de Colisão

        public int obterMascaraColisao(string nomeLayer)
        {
            return LayerMask.GetMask(nomeLayer);
        }

        public int obterMascaraBlocoArqueologico()
        {
            if (mascaraBlocoArqueologico == MASCARA_NAO_ENCONTRADA)
                mascaraBlocoArqueologico = obterMascaraColisao(NOME_LAYER_BLOCO_ARQUEOLOGICO);

            return mascaraBlocoArqueologico;
        }

        public int obterMascaraChao()
        {
            if (mascaraChao == MASCARA_NAO_ENCONTRADA)
                mascaraChao = obterMascaraColisao(NOME_LAYER_CHAO);

            return mascaraChao;
        }

        #endregion


        public GameObject obterBlocoPorRaycast(Vector3 posicao, Vector3 direcao)
        {
            Ray ray = new Ray(posicao, direcao);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, UtilitariosGamePlay.UNIDADE_TAMANHO_CUBO * tamanhoMinimoRaio, obterMascaraBlocoArqueologico()))
                return hit.collider.gameObject;

            return null;
        }

        public Collider[] colisaoCubica(Vector3 posicao, Vector3 metadeTamanhoCubo, Quaternion rotacao)
        {
            return Physics.OverlapBox(posicao, metadeTamanhoCubo, rotacao, obterMascaraBlocoArqueologico());
        }

        public Collider[] colisaoCubica(Vector3 posicao, Vector3 metadeTamanhoCubo)
        {
            return colisaoCubica(posicao, metadeTamanhoCubo, Quaternion.identity);
        }

        public Collider[] colisaoPonto(Vector3 ponto)
        {
            return colisaoCubica(ponto, Vector3.zero, Quaternion.identity);
        }

        public Collider pegarPrimeiroCollider(Collider[] collider)
        {
            return collider.Length > 0 ? collider[0] : null;
        }

        public GameObject pegarPrimeiroObjeto(Collider[] collider)
        {
            return collider.Length > 0 ? collider[0].gameObject : null;
        }

        public List<GameObject> transformarCollidersEmGameObjects(Collider[] collider)
        {
            List<GameObject> objs = new List<GameObject>(); 
            foreach(var i in collider)
            {
                objs.Add(i.gameObject);
            }

            return objs;
        }

        public bool checarSeEspacoEstaOcupado(Vector3 ponto)
        {
            return colisaoPonto(ponto).Length > 0;
        }

        public bool checarSeEspacoEstaVazio(Vector3 ponto)
        {
            return !checarSeEspacoEstaOcupado(ponto);
        }
        
        public bool checarCuboEstaNoChao(GameObject obj)
        {
            Collider[] col = Physics.OverlapBox(obj.gameObject.transform.position + new Vector3(0, UtilitariosGamePlay.UNIDADE_METADE_TAMANHO_CUBO, 0), Vector3.one / 5 , Quaternion.identity, obterMascaraChao());
            return col.Length > 0;
        }

        public bool checarBlocoAbaixo(GameObject obj)
        {
            Ray ray = new Ray(obj.transform.position, Vector3.down);
            return Physics.Raycast(ray, tamanhoMinimoRaio, obterMascaraBlocoArqueologico());
        }

        public void posicionarBlocoAcimaDoChao(GameObject obj) {
            Ray ray = new Ray(obj.transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, tamanhoMinimoRaio, obterMascaraChao()))
                obj.transform.position = new Vector3(obj.transform.position.x, hit.collider.gameObject.transform.position.y + (UtilitariosGamePlay.UNIDADE_TAMANHO_CUBO / 2), obj.transform.position.z);
        }
        
        public void posicionarBlocoAbaixoDoChao(GameObject obj) {
            Ray ray = new Ray(obj.transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, tamanhoMinimoRaio, obterMascaraChao()))
                obj.transform.position = new Vector3(obj.transform.position.x, hit.collider.gameObject.transform.position.y - (UtilitariosGamePlay.UNIDADE_TAMANHO_CUBO / 2), obj.transform.position.z);

        }
    
        public void cairSobOutroCubo(GameObject obj, bool aplicarDano)
        {
            Ray ray = new Ray(obj.transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, tamanhoMinimoRaio, obterMascaraBlocoArqueologico()))
            {
                var blocInfo = hit.collider.gameObject.GetComponent<BlocoGenerico>();
                if (aplicarDano && blocInfo.BlocoSO.SofreDanoQuandoCuboCaiNele && blocInfo.tomarDano(UtilitariosGamePlay.DANO_QUEDA_BLOCO_SOB_BLOCO))
                {
                    cairSobOutroCubo(obj, aplicarDano);
                }
                else
                {
                    obj.transform.position = hit.collider.gameObject.transform.position + Vector3.up;
                }
            }
            else
                posicionarBlocoAcimaDoChao(obj);
        }
    }
}
