using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe {
    /// <summary>
    /// Classe que define o comportamento do bloco explosivo, incluindo referência à ferramenta explosiva utilizada.
    /// </summary>
    public class BlocoExplosivo : BlocoGenerico
    {
        [SerializeField] private FerramentaSO ferramentaExplosiva;

        public FerramentaSO FerramentaExplosiva => ferramentaExplosiva; 
    }
}
