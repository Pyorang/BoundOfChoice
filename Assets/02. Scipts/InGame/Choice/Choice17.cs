using UnityEngine;

public class Choice17 : ChoiceBase
{
    private static readonly float ASpawnRepeatChance = 0.65f;
    private static readonly float _spawnDelay = 1.5f;
    private static readonly WaitForSeconds _waitDelayTime = new WaitForSeconds(_spawnDelay);

    protected override void StepA()
    {
        MonsterSpawner.Instance.SpawnMonstersWithDelay(EPoolType.SkeletonSwrodsman, _waitDelayTime, ASpawnRepeatChance);
    }

    protected override void StepB()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonNecro);
    }
}
