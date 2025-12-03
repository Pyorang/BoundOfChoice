using UnityEngine;

public class Choice16 : ChoiceBase
{
    private static readonly int AActivateTrapAmount = 2;

    protected override void StepLeft()
    {
        for(int i = 0; i < AActivateTrapAmount; i++)
        {
            KnightTrapManager.Instance.Activate();
        }

        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonSwrodsman);
    }

    protected override void StepRight()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonElite);
    }
}
