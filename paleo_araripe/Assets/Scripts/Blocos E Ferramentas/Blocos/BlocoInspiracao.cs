using UnityEngine;

namespace PaleoAraripe {
    /// <summary>
    /// Classe que define o comportamento do bloco de inspiração, responsável por conceder pontos de inspiração ao jogador.
    /// </summary>
    public class BlocoInspiracao : BlocoGenerico
    {
        [SerializeField] private int inspiracao = 1;

        public int Inspiracao => inspiracao; 
    }
}
