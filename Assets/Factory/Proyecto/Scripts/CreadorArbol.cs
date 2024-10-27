using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreadorArbol : Factory
{
    public override IBaseClass CrearItemElegido()
    {
        return new Arbol();
    }
}
