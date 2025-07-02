using UnityEngine;

namespace PaleoAraripe {
    /// <summary>
    /// Classe que define o comportamento do bloco de areia, incluindo lógica de queda e verificação de contato com o chão.
    /// </summary>
    public class BlocoAreia : BlocoGenerico
    {
        private ColisoesBlocosChao col = new ColisoesBlocosChao();
        private bool jaEstaNoChao = false;
        
        // Getters
        public bool JaEstaNoChao => jaEstaNoChao; 

        
        public void Start()
        {
            jaEstaNoChao = col.checarCuboEstaNoChao(gameObject);
        }

        
        private bool checarBlocoAbaixo()
        {
            return col.colisaoPonto(transform.position + Vector3.down).Length > 0;
        }

        public bool cair()
        {
            if (jaEstaNoChao || checarBlocoAbaixo())
                return false;

            if (col.checarBlocoAbaixo(gameObject)) 
            { 
                col.cairSobOutroCubo(gameObject, true);
            }
            else 
            { 
                col.posicionarBlocoAcimaDoChao(gameObject);
                jaEstaNoChao = true;
            }



            return true;
        }
    }
}
