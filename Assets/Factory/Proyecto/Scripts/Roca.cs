using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roca : MonoBehaviour, IBaseClass
{
    public GameObject RocaItem;
    public GameObject CrearItem(GameObject item, Vector3 spawner)
    {
        return Object.Instantiate(item, spawner, Quaternion.identity) as GameObject;
    }
}
