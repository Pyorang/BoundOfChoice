using UnityEngine;

public class Choice20 : ChoiceBase
{
    protected override void StepLeft()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.BringerOfDeath);
    }

    protected override void StepRight()
    {
        MonsterSpawner.Instance.SpawnMonsters(
            stackalloc EPoolType[]
            {
                EPoolType.SkeletonArbalist,
                EPoolType.SkeletonNecro,
                EPoolType.SkeletonElite
            }
        );
    }
}
