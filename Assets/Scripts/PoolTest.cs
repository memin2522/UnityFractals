using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    [SerializeField] ObjectPool pools;
    public void TestPool(string tag)
    {
        pools.GetObjectFromPool(tag);
    }

    
}
