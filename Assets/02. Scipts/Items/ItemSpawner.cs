using System.Collections.Generic;
using UnityEngine;

public enum EItemType
{
    None,
    Apple,
    HealthPotion,
    ManaPotion,
}

[System.Serializable]
public struct ItemPrefabInfo
{
    public EItemType ItemType;
    public GameObject Prefab;
}

public class ItemSpawner : SingletonBehaviour<ItemSpawner>
{
    [SerializeField] private List<ItemPrefabInfo> _itemPrefabInfos;
    private Dictionary<EItemType, GameObject> _itemPrefabDict;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();

        _itemPrefabDict = new Dictionary<EItemType, GameObject>();
        foreach (var info in _itemPrefabInfos)
        {
            if (!_itemPrefabDict.TryAdd(info.ItemType, info.Prefab))
            {
                Debug.LogWarning($"중복된 아이템({info.ItemType})이 존재합니다.");
            }
        }
    }

    public GameObject CreateItem(EItemType itemType, Vector2 position)
    {
        if (!_itemPrefabDict.TryGetValue(itemType, out GameObject prefab) || prefab == null)
        {
            Debug.LogError($"아이템({itemType})이 없거나 프리팹이 null 입니다.");
            return null;
        }

        GameObject monster = Instantiate(prefab, position, Quaternion.identity, transform);

        return monster;
    }
}
