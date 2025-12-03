using UnityEngine;

public class Choice20 : ChoiceBase
{
    protected override void StepLeft()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.BringerOfDeath);
    }

    protected override void StepRight()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonElite);
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonNecro);
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonArbalist);
    }
}
