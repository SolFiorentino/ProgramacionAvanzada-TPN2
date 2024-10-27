using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbol : MonoBehaviour, IBaseClass
{
    public GameObject ArbolItem;
    public GameObject CrearItem(GameObject item, Vector3 spawner)
    {
        return Object.Instantiate(item, spawner, Quaternion.identity) as GameObject;
    }
}
