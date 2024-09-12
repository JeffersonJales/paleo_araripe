using System;
using UnityEngine;

namespace Utilidades
{
    public class ColisoesBlocosChao 
    {
        public const string NOME_LAYER_BLOCO_ARQUEOLOGICO = "BlocoArqueologico";
        public const string NOME_LAYER_CHAO = "Chao";

        private const int MASCARA_NAO_ENCONTRADA = -1;
        private int mascaraBlocoArqueologico = MASCARA_NAO_ENCONTRADA;
        private int mascaraChao = MASCARA_NAO_ENCONTRADA;

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

        public Collider[] colisaoCubica(Vector3 posicao, Vector3 metadeTamanhoCubo, Quaternion rotacao)
        {
            return Physics.OverlapBox(posicao, metadeTamanhoCubo, rotacao, obterMascaraBlocoArqueologico());
        }

        public Collider[] colisaoPonto(Vector3 ponto)
        {
            return colisaoCubica(ponto, Vector3.zero, Quaternion.identity);
        }

        public Collider pegarPrimeiroCollider(Collider[] collider)
        {
            return collider.Length > 0 ? collider[0] : null;
        }

        public Boolean checarCuboEstaNoChao(GameObject obj)
        {
            Collider[] col = Physics.OverlapBox(obj.gameObject.transform.position + (obj.gameObject.transform.localScale / 2), new Vector3(0.1f, 0.1f, 0.1f), Quaternion.identity, obterMascaraChao());
            return col.Length > 0;
        }
    }
}
