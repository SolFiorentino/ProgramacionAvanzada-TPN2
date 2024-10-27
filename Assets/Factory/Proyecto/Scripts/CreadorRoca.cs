using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreadorRoca : Factory
{
    public override IBaseClass CrearItemElegido()
    {
        return new Roca();
    }
}
