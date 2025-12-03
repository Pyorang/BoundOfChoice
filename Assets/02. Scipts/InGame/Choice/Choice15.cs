using UnityEngine;

public class Choice15 : ChoiceBase
{
    private static readonly int AGoldAmount = 20;
    private static readonly int ASkeletonCount = 2;

    protected override void StepLeft()
    {
        GoldManager.Instance.GetGold(AGoldAmount);
        MonsterSpawner.Instance.SpawnRandomSkeletons(ASkeletonCount);
    }

    protected override void StepRight()
    {
        MonsterSpawner.Instance.SpawnMonsters(
            stackalloc EPoolType[]
            {
                EPoolType.SkeletonSwordsman,
                EPoolType.SkeletonSwordsman
            }
        );
    }
}
