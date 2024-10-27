using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreadorArbusto : Factory
{
    public GameObject Arbusto;
    public override IBaseClass CrearItemElegido()
    {
        return new Arbusto();
    }
}
