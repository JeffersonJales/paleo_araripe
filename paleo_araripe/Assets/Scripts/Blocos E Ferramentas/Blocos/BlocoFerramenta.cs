using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe {
    /// <summary>
    /// Classe que define o comportamento do bloco ferramenta, responsável por liberar uma ferramenta ao ser coletado.
    /// </summary>
    public class BlocoFerramenta : BlocoGenerico
    {
        [SerializeField] private FerramentaSO ferramenta;

        public override void aoSerColetado()
        {
            
            base.aoSerColetado();
        }
    }
}
