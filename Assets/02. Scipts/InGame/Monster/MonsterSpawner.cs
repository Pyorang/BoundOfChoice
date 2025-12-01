using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MonsterPrefabInfo
{
    public int ID;
    public GameObject Prefab;
}

public class MonsterSpawner : SingletonBehaviour<MonsterSpawner>
{
    private int _currentMonsterCount = 0;
    public int CurrentMonsterCount
    {
        get { return _currentMonsterCount; }
        set
        {
            _currentMonsterCount = value;

            if(_currentMonsterCount == 0)
            {
                ChoiceManager.Instance.GetNewChoice();
                Angel.Instance.EnableLevers();
            }

            else
            {
                Angel.Instance.DisableLevers();
            }
        }
    }

    [SerializeField] private List<MonsterPrefabInfo> _monsterPrefabInfos;
    private Dictionary<int, GameObject> _monsterPrefabDict;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();

        // NOTE : 인스펙터 창에서 입력된 프리팹 정보를 Dictionary로 저장한다. 
        _monsterPrefabDict = new Dictionary<int, GameObject>();
        foreach (var info in _monsterPrefabInfos)
        {
            if (!_monsterPrefabDict.TryAdd(info.ID, info.Prefab))
            {
                Debug.LogWarning($"중복된 몬스터 ID({info.ID})가 존재합니다.");
            }
        }
    }

    public GameObject CreateMonster(int id, Vector2 position)
    {
        CurrentMonsterCount++;

        if (!_monsterPrefabDict.TryGetValue(id, out GameObject prefab) || prefab == null)
        {
            Debug.LogError($"몬스터 ID({id})가 없거나 프리팹이 null 입니다.");
            return null;
        }

        GameObject monster  = Instantiate(prefab, position, Quaternion.identity, transform);

        return monster;
    }
}
