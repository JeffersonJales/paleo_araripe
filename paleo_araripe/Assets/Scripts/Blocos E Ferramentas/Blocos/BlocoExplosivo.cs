using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoExplosivo : BlocoGenerico
{
    [SerializeField] private FerramentaSO ferramentaExplosiva;

    public FerramentaSO FerramentaExplosiva => ferramentaExplosiva; 
}
