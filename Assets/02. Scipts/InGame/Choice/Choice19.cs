using UnityEngine;

public class Choice19 : ChoiceBase
{
    private static readonly int BDamageAmount = 25;

    protected override void StepA()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonSwrodsman);
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonArbalist);
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonArbalist);
    }

    protected override void StepB()
    {
        PlayerHealth.Instance.TakeDamage(BDamageAmount);
    }
}
