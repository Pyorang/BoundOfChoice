using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct MonsterPrefabInfo
{
    public int ID;
    public GameObject Prefab;
}

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private List<MonsterPrefabInfo> _monsterPrefabInfos;
    private Dictionary<int, GameObject> _monsterPrefabDict;

    private void Awake()
    {
        _monsterPrefabDict = new Dictionary<int, GameObject>();
        foreach(var info in _monsterPrefabInfos)
        {
            if (_monsterPrefabDict.ContainsKey(info.ID))
            {
                Debug.LogWarning($"중복된 몬스터 ID({info.ID})가 존재합니다.");
            }
            else
            {
                _monsterPrefabDict.Add(info.ID, info.Prefab);
            }
        }
        _monsterPrefabInfos.Clear();
    }

    public GameObject CreateMonster(int id, Vector2 position, Quaternion rotation)
    {
        if (_monsterPrefabDict.TryGetValue(id, out GameObject prefab) || prefab == null)
        {
            Debug.LogError($"몬스터 ID({id})가 없거나 프리팹이 null 입니다.");
            return null;
        }

        return Instantiate(prefab, position, rotation, transform);
    }
}
