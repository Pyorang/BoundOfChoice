using UnityEngine;

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
}
