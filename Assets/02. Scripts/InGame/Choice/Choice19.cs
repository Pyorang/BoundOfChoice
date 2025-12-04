using UnityEngine;

public class Choice19 : ChoiceBase
{
    private static readonly int BDamageAmount = 25;

    protected override void StepLeft()
    {
        MonsterSpawner.Instance.SpawnMonsters(
            stackalloc EPoolType[]
            {
                EPoolType.SkeletonArbalist,
                EPoolType.SkeletonArbalist,
                EPoolType.SkeletonSwordsman
            }
        );
    }

    protected override void StepRight()
    {
        PlayerHealth.Instance.TakeDamage(BDamageAmount);
    }
}
