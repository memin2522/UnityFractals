using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<ObjectInPool> objectsInPool = new List<ObjectInPool>();

    private void Start()
    {
        if (objectsInPool.Count > 0)
        {
            foreach (var pool in objectsInPool)
            {
                if (pool.objectsInstance == null)
                {
                    pool.objectsInstance = new Queue<GameObject>();
                }

                for (int i = 0; i < pool.poolLength; i++)
                {
                    InstanceObjectInPool(pool);
                }
            }
        }
    }

    private GameObject InstanceObjectInPool(ObjectInPool pool)
    {
        GameObject currentInstance = Instantiate(pool.prefab, transform);
        currentInstance.SetActive(false);
        pool.objectsInstance.Enqueue(currentInstance);
        return currentInstance;
    }

    public GameObject GetObjectFromPool(string tag)
    {
        foreach (var pool in objectsInPool)
        {
            if (pool.poolTag == tag)
            {
                GameObject objectFromPool;
                if (pool.objectsInstance.Count > 0)
                {
                    objectFromPool = pool.objectsInstance.Dequeue();
                }
                else
                {
                    objectFromPool = Instantiate(pool.prefab, transform);
                }
                objectFromPool.SetActive(true);
                return objectFromPool;
            }
        }
        Debug.LogWarning("No pool found with tag: " + tag);
        return null;
    }
}

[Serializable]
public class ObjectInPool
{
    public string poolTag;
    public GameObject prefab;
    public int poolLength;
    public Queue<GameObject> objectsInstance = new Queue<GameObject>();
}