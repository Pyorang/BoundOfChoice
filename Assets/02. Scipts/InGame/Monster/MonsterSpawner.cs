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
    [SerializeField] private List<MonsterPrefabInfo> _monsterPrefabInfos;
    private Dictionary<int, GameObject> _monsterPrefabDict;

    [SerializeField] private GameObject _player;
    private const string PlayerTag = "player";

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

    private void Start()
    {
        // NOTE : player가 인스펙터에서 할당되지 않으면 태그로 탐색하여 캐싱한다.
        if (_player == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(PlayerTag);
            if (player != null)
            {
                _player = player;
            }
            else
            {
                Debug.LogError($"'{PlayerTag}' 태그를 가진 플레이어를 찾을 수 없습니다. Player 오브젝트에 태그가 올바르게 설정되었는지 확인해주세요. ");
            }
        }
    }

    public GameObject CreateMonster(int id, Vector2 position)
    {
        if (!_monsterPrefabDict.TryGetValue(id, out GameObject prefab) || prefab == null)
        {
            Debug.LogError($"몬스터 ID({id})가 없거나 프리팹이 null 입니다.");
            return null;
        }

        GameObject monster  = Instantiate(prefab, position, Quaternion.identity, transform);
        
        if (monster.TryGetComponent<MonsterController>(out var controller))
        {
            controller.SetPlayer(_player);
        }
        else
        {
            Debug.LogError($"몬스터 프리팹(ID : {id})에 MonsterController 컴포넌트가 없습니다.");
            Destroy(monster);
            return null;
        }

        return monster;
    }
}
