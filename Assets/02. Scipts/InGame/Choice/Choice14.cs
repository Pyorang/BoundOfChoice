using UnityEngine;

public class Choice14 : ChoiceBase
{
    private static readonly int AGoldAmount = 10;

    protected override void StepLeft()
    {
        GoldManager.Instance.GetGold(AGoldAmount);
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonSwrodsman);
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonSwrodsman);
    }

    protected override void StepRight()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonSwrodsman);
    }
}
