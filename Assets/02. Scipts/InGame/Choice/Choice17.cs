using UnityEngine;

public class Choice17 : ChoiceBase
{
    private static readonly float ASpawnRepeatChance = 0.65f;

    protected override void StepLeft()
    {
        do
        {
            MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonSwrodsman);
        } while (Random.value <= ASpawnRepeatChance);
    }

    protected override void StepRight()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonNecro);
    }
}
