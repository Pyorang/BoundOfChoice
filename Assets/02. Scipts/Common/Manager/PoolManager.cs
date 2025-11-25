using System.Collections.Generic;
using UnityEngine;

public enum EPoolType
{
    Arrow,
    MagicBolt,
    FireBall,
}

[System.Serializable] public struct PoolInfo
{
    public EPoolType Type;
    public GameObject PoolObjectPrefab;
    public int InitialCount;
}

public class PoolManager : SingletonBehaviour<PoolManager>
{
    [SerializeField] private PoolInfo[] _poolInfos;
    private Dictionary<EPoolType, ObjectPool> _pools = new Dictionary<EPoolType, ObjectPool>();

    protected override void Init()
    {
        base.Init();
        foreach (var info in _poolInfos)
        {
            GameObject folder = new GameObject { name = info.PoolObjectPrefab.name };
            folder.transform.parent = this.transform;
            ObjectPool pool = new ObjectPool();
            pool.InitPool(info.PoolObjectPrefab, info.InitialCount, folder.transform);
            _pools.Add(info.Type, pool);
        }
    }

    public GameObject GetObject(EPoolType type, bool active = true)
    {
        if (_pools.TryGetValue(type, out ObjectPool pool))
        {
            GameObject poolObject = pool.GetObject();
            poolObject.SetActive(active);
            return poolObject;
        }
        return null;
    }

    public void ReleaseObject(EPoolType type, GameObject poolObject)
    {
        if (_pools.ContainsKey(type) == false) return;
        poolObject.SetActive(false);
        _pools[type].ReleaseObject(poolObject);
    }
}
