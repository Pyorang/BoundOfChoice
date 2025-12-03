using UnityEngine;

public class Choice17 : ChoiceBase
{
    private static readonly float s_spawnRepeatChance = 0.65f;
    private static readonly float s_spawnDelay = 1.5f;
    private static readonly WaitForSeconds s_waitDelayTime = new WaitForSeconds(s_spawnDelay);

    protected override void StepA()
    {
        MonsterSpawner.Instance.SpawnMonstersWithDelay(EPoolType.SkeletonSwrodsman, s_waitDelayTime, s_spawnRepeatChance);
    }

    protected override void StepB()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonNecro);
    }
}
