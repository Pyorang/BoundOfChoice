using System;
using System.Collections;
using UnityEngine;

public class MonsterSpawner : SingletonBehaviour<MonsterSpawner>
{
    [Header("몬스터 스폰 위치")]
    [SerializeField] private float _minSpawnX;
    [SerializeField] private float _maxSpawnX;
    private const float HalfRate = 0.5f;

    [Header("몬스터 간격")]
    [SerializeField] private float _monsterSpacing;
    [SerializeField] private float _maxSpawnWidth;

    private static readonly string SpawnSound = "SkeletonSpawn";
    private static readonly string BringerSpawnSound = "BringerSpawn";

    private static readonly EPoolType[] SkeletonTypes = new EPoolType[]
    {
        EPoolType.SkeletonSwordsman,
        EPoolType.SkeletonArbalist,
        EPoolType.SkeletonNecro,
        EPoolType.SkeletonElite
    };

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

    public GameObject SpawnMonster(EPoolType monsterType, float positionX = float.NaN)
    {
        ++CurrentMonsterCount;

        GameObject monster = PoolManager.Instance.GetObject(monsterType);

        if(float.IsNaN(positionX))
        {
            string soundToPlay = monsterType == EPoolType.BringerOfDeath ? BringerSpawnSound : SpawnSound;
            AudioManager.Instance.Play(AudioType.SFX, soundToPlay);
            positionX = UnityEngine.Random.Range(_minSpawnX, _maxSpawnX);
        }

        monster.transform.position = new Vector3(
            positionX,
            monster.transform.position.y,
            monster.transform.position.z
        );

        return monster;
    }

    public void SpawnMonsters(ReadOnlySpan<EPoolType> monsterTypes)
    {
        if (monsterTypes.Length <= 1) return;
        AudioManager.Instance.Play(AudioType.SFX, SpawnSound);

        float baseSpawnX = transform.position.x;

        float totalWidth = (monsterTypes.Length - 1) * _monsterSpacing;
        totalWidth = Math.Min(totalWidth, _maxSpawnWidth);

        float monsterSpacing = totalWidth / (monsterTypes.Length - 1);

        float startOffset = -totalWidth / 2f;

        float sign = UnityEngine.Random.value < HalfRate ? 1 : -1;

        for (int i = 0; i < monsterTypes.Length; ++i)
        {
            float spawnX = baseSpawnX + startOffset + i * monsterSpacing;
            SpawnMonster(monsterTypes[i], spawnX * sign);
        }
    }

    public void SpawnRandomSkeletons(int count)
    {
        if (count <= 0) return;

        Span<EPoolType> spawnList = stackalloc EPoolType[count];

        for (int i = 0; i < count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, SkeletonTypes.Length);
            spawnList[i] = SkeletonTypes[randomIndex];
        }

        SpawnMonsters(spawnList);
    }

    public void SpawnMonstersWithDelay(EPoolType monsterType, WaitForSeconds spawnDelayTime, float spawnRepeatChance)
    {
        StartCoroutine(SpawnMonstersCoroutine(monsterType, spawnDelayTime, spawnRepeatChance));
    }

    private IEnumerator SpawnMonstersCoroutine(EPoolType monsterType, WaitForSeconds spawnDelayTime, float spawnRepeatChance)
    {
        do
        {
            SpawnMonster(monsterType);

            yield return spawnDelayTime;

        } while (UnityEngine.Random.value <= spawnRepeatChance);
    }
}
