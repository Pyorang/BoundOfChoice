using UnityEngine;

public class Choice16 : ChoiceBase
{
    private static readonly int AActivateTrapAmount = 2;

    protected override void StepA()
    {
        for(int i = 0; i < AActivateTrapAmount; i++)
        {
            KnightTrapManager.Instance.Activate();
        }

        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonSwrodsman);
    }

    protected override void StepB()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonElite);
    }
}
