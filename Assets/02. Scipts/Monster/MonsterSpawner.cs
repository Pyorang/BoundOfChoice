using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private Transform _player;

    private void Awake()
    {
        // NOTE : 인스펙터 창에서 입력된 프리팹 정보를 Dictionary로 저장한다. 
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
        if (!_monsterPrefabDict.TryGetValue(id, out GameObject prefab) || prefab == null)
        {
            Debug.LogError($"몬스터 ID({id})가 없거나 프리팹이 null 입니다.");
            return null;
        }

        GameObject monster  = Instantiate(prefab, position, rotation, transform);
        monster.GetComponent<MonsterController>().SetTargetTransform(_player);

        return monster;
    }

    public void TestButton()
    {
        CreateMonster(0, new Vector2(-3, 0), Quaternion.identity);
    }
}
