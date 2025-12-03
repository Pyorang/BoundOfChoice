using UnityEngine;

public class Choice15 : ChoiceBase
{
    private static readonly int AGoldAmount = 20;

    protected override void StepLeft()
    {
        GoldManager.Instance.GetGold(AGoldAmount);
        Debug.Log("랜덤 스켈레톤 두 마리 소환");
    }

    protected override void StepRight()
    {
        MonsterSpawner.Instance.SpawnMonsters(
            stackalloc EPoolType[]
            {
                EPoolType.SkeletonSwrodsman,
                EPoolType.SkeletonSwrodsman
            }
        );
    }
}
