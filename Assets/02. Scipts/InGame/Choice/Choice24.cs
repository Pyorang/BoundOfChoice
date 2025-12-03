using UnityEngine;

public class Choice24 : ChoiceBase
{
    private static readonly int ArcherAlreadyGainedGoldAmount = 50;

    protected override void StepLeft()
    {
        PlayerCombat.Instance.OpenCharacter(ECharacterType.Archer, ArcherAlreadyGainedGoldAmount);
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonArbalist);
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonArbalist);
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonArbalist);
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonArbalist);
    }

    protected override void StepRight()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonArbalist);
    }
}
