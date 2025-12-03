using UnityEngine;

public class Choice19 : ChoiceBase
{
    private static readonly int BDamageAmount = 25;

    protected override void StepA()
    {
        MonsterSpawner.Instance.SpawnMonsters(
            stackalloc EPoolType[]
            {
                EPoolType.SkeletonSwrodsman,
                EPoolType.SkeletonArbalist,
                EPoolType.SkeletonArbalist
            }
        );
    }

    protected override void StepB()
    {
        PlayerHealth.Instance.TakeDamage(BDamageAmount);
    }
}
