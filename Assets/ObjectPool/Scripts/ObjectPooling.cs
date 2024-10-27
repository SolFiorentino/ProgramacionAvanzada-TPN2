using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance { get; private set; }

    public static ObjectPooling SharedInstance;
    public List<List<GameObject>> PoolsPooling;
    public List<GameObject> BulletPrefabs;
    public int amountToPool;
    public List<int> amountsToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        PoolsPooling = new List<List<GameObject>>();
        InitializePools();
    }

    void InitializePools()
    {
        foreach (GameObject prefab in BulletPrefabs)
        {
            List<GameObject> bulletType = new List<GameObject>();
            for (int i = 0; i < amountToPool; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                bulletType.Add(obj);
            }
            PoolsPooling.Add(bulletType);
        }
    }

    public GameObject GetPooledObject(int bulletType)
    {
        List<GameObject> NewList = PoolsPooling[bulletType - 1];

        for (int i = 0; i < NewList.Count; i++)
        {
            if (!NewList[i].activeInHierarchy)
            {
                return NewList[i];
            }
        }
        return null;
    }
}
