using System;
using UnityEngine;

public class MonsterSpawner : SingletonBehaviour<MonsterSpawner>
{
    [Header("몬스터 간격")]
    [SerializeField] private float _monsterSpacing;

    private int _currentMonsterCount = 0;
    public int CurrentMonsterCount
    {
        get { return _currentMonsterCount; }
        set
        {
            _currentMonsterCount = value;

            if (_currentMonsterCount == 0)
            {
                ChoiceManager.Instance.GetNewChoice();
            }

            else
            {
                Angel.Instance.DisableLevers();
            }
        }
    }

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    public GameObject SpawnMonster(EPoolType monsterType)
    {
        CurrentMonsterCount++;

        GameObject monster = PoolManager.Instance.GetObject(monsterType);
        
        Vector3 spawnPosition = monster.transform.position;
        spawnPosition.x = transform.position.x;
        monster.transform.position = spawnPosition;

        return monster;
    }

    public GameObject SpawnMonster(EPoolType monsterType, float positionX)
    {
        ++CurrentMonsterCount;

        GameObject monster = PoolManager.Instance.GetObject(monsterType);

        monster.transform.position = new Vector3(
            positionX,
            monster.transform.position.y,
            monster.transform.position.z
        );

        return monster;
    }

    public void SpawnMonsters(ReadOnlySpan<EPoolType> monsterTypes)
    {
        float SpawnX = transform.position.x;

        float totalWidth = (monsterTypes.Length - 1) * _monsterSpacing;

        float startOffset = -totalWidth / 2f;

        for (int i = 0; i < monsterTypes.Length; ++i)
        {
            float offsetX = SpawnX + startOffset + i * _monsterSpacing;

            SpawnMonster(monsterTypes[i], offsetX);
        }
    }
}
