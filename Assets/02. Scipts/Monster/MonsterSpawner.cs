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

    public GameObject CreateMonster(int id, Vector2 position, Quaternion rotation)
    {
        if (_monsterPrefabInfos == null)
        {
            Debug.LogError("Monster Prefab list is null.");
            return null;
        }

        GameObject prefab = null;
        foreach (var info in _monsterPrefabInfos)
        {
            if (info.ID == id)
            {
                prefab = info.Prefab;
                break;
            }
        }

        if (prefab == null)
        {
            Debug.LogError($"Monster ID : {id} not found or prefab is null.");
            return null;
        }

        GameObject monster = Instantiate(prefab, transform);
        monster.transform.position = position;
        monster.transform.rotation = rotation;

        return monster;
    }
}
