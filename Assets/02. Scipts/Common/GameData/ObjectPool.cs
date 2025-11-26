using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private Queue<GameObject> _pool;
    private HashSet<GameObject> _activeObjects = new HashSet<GameObject>();

    private GameObject _prefab = null;
    private Transform _parent = null;

    private int _poolCount;
    private const float IncreasePoolRate = 1.5f;

    private void CreateObject(int newObjectCount)
    {
        for (int i = 0; i < newObjectCount; ++i)
        {
            GameObject newObject = GameObject.Instantiate(_prefab, _parent);
            newObject.SetActive(false);
            _pool.Enqueue(newObject);
        }
    }

    public void InitPool(GameObject prefab, int poolCount, Transform parent = null)
    {
        _poolCount = poolCount;
        _prefab = prefab;
        _parent = parent;
        _pool = new Queue<GameObject>(poolCount);

        CreateObject(poolCount);
    }

    public GameObject GetObject()
    {
        if (_pool.Count == 0)
        {
            int newPoolCount = Mathf.Max(_poolCount + 1, (int)(_poolCount * IncreasePoolRate));
            CreateObject(newPoolCount - _poolCount);
            _poolCount = newPoolCount;
        }
        GameObject pooledObject = _pool.Dequeue();
        _activeObjects.Add(pooledObject);
        return pooledObject;
    }

    public void ReleaseObject(GameObject releaseObject)
    {
        if (!_activeObjects.Contains(releaseObject)) return;
        _activeObjects.Remove(releaseObject);
        _pool.Enqueue(releaseObject);
    }
}
