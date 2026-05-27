using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ObjectPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Queue<T> _availableObjects = new();
    private readonly HashSet<T> _objectsInPool = new();
    private readonly int _maxSize;
    private int _totalAmount;
    public int TotalAmount => _totalAmount;

    public ObjectPool(T prefab, int startCount, Transform parent, int maxSize)
    {
        _maxSize = Mathf.Max(1, maxSize);
        startCount = Mathf.Clamp(startCount, 0, _maxSize);
        _prefab = prefab;
        _parent = parent;

        for ( int i = 0; i < startCount; i++ )
        {
            T obj = CreateObj();
            obj.gameObject.SetActive( false );
            _objectsInPool.Add(obj);
            _availableObjects.Enqueue(obj);
        }
    }
    private T CreateObj()
    {
        T obj = Object.Instantiate(_prefab, _parent);
        _totalAmount++;
        return obj;
    }
    public T Get()
    {
        T obj = null;
        if (_availableObjects.Count > 0)
        {
            obj = _availableObjects.Dequeue();
            _objectsInPool.Remove(obj);
        }
        else if (_totalAmount < _maxSize)
        {
            obj = CreateObj();
        }
        if (obj == null)
        {
            return obj;
        }
        obj.gameObject.SetActive( true );

        if (obj.TryGetComponent<IPoolable>(out var poolable))
        {
            poolable.OnSpawnFromPool();
        }
        return obj;
    }
    private void DestroyObject(T obj)
    {
        GameObject.Destroy(obj.gameObject);
        _totalAmount--;
    }
    public void Return(T obj)
    {
        if (obj == null)
        {
            return;
        }
        if (_objectsInPool.Contains(obj))
        {
            Debug.LogWarning($"{obj} is already in the {nameof(ObjectPool<T>)} ");
            return;  
        }
        if (_availableObjects.Count >= _maxSize)
        {
            DestroyObject(obj);
            return;
        }
        if (obj.TryGetComponent<IPoolable>(out var poolable))
        {
            poolable.OnReturnToPool();
        }
        obj.transform.SetParent(_parent);
        obj.gameObject.SetActive(false);
        _objectsInPool.Add(obj);
        _availableObjects.Enqueue(obj);
    }
}
