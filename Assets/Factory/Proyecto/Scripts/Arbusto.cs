using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbusto : MonoBehaviour, IBaseClass
{
    public GameObject ArbustoItem;
    public GameObject CrearItem(GameObject item, Vector3 spawner)
    {
        return Object.Instantiate(item, spawner, Quaternion.identity) as GameObject;
    }
}
