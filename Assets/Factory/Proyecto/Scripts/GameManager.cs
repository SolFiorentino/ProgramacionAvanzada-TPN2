using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Spawner;
    public GameObject Roca;
    public GameObject Arbol;
    public GameObject Arbusto;

    public void CreacionRoca()
    {
        Factory factory = new CreadorRoca();
        IBaseClass baseClass = factory.CrearItemElegido();
        baseClass.CrearItem(Roca, Spawner.transform.position);
    }

    public void CreacionArbol()
    {
        Factory factory = new CreadorArbol();
        IBaseClass baseClass = factory.CrearItemElegido();
        baseClass.CrearItem(Arbol, Spawner.transform.position);
    }

    public void CreacionArbusto()
    {
        Factory factory = new CreadorArbusto();
        IBaseClass baseClass = factory.CrearItemElegido();
        baseClass.CrearItem(Arbusto, Spawner.transform.position);
    }
}
