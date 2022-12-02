using System;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] private PoolableObject poolObject;
    [SerializeField] private Transform poolParent;

    ObjectPool<PoolableObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<PoolableObject>(CreateObject, OnTakeObjectFromPool, OnReturnObjectToPool);
    }

    private void OnReturnObjectToPool(PoolableObject obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnTakeObjectFromPool(PoolableObject obj)
    {
        obj.gameObject.SetActive(true);
    }

    public PoolableObject CreateObject()
    {
        var newObj = Instantiate(poolObject, poolParent);
        return newObj;
    }

    public void Release(PoolableObject poolableObject)
    {
        if (_pool != null)
        {
            _pool.Release(poolableObject);
        }
    }

    public PoolableObject Get()
    {
        PoolableObject newObj = null;
        if (_pool != null)
        {
            newObj = _pool.Get();
        }
        return newObj;
    }

}
