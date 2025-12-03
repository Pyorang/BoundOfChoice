using UnityEngine;

public class Choice20 : ChoiceBase
{
    protected override void StepA()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.BringerOfDeath);
    }

    protected override void StepB()
    {
        MonsterSpawner.Instance.SpawnMonsters(
            stackalloc EPoolType[]
            {
                EPoolType.SkeletonElite,
                EPoolType.SkeletonNecro,
                EPoolType.SkeletonArbalist
            }
        );
    }
}
