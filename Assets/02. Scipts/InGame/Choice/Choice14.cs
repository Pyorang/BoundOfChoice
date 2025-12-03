using UnityEngine;

public class Choice14 : ChoiceBase
{
    private static readonly int AGoldAmount = 10;

    protected override void StepLeft()
    {
        GoldManager.Instance.GetGold(AGoldAmount);
        MonsterSpawner.Instance.SpawnMonsters(
            stackalloc EPoolType[]
            {
                EPoolType.SkeletonSwrodsman,
                EPoolType.SkeletonSwrodsman
            }
        );
    }

    protected override void StepRight()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonSwrodsman);
    }
}
