using UnityEngine;

public class Choice24 : ChoiceBase
{
    private static readonly int ArcherAlreadyGainedGoldAmount = 50;

    protected override void StepA()
    {
        PlayerCombat.Instance.OpenCharacter(ECharacterType.Archer, ArcherAlreadyGainedGoldAmount);
        MonsterSpawner.Instance.SpawnMonsters(
            stackalloc EPoolType[]
            {
                EPoolType.SkeletonArbalist,
                EPoolType.SkeletonArbalist,
                EPoolType.SkeletonArbalist,
                EPoolType.SkeletonArbalist
            }
        );
    }

    protected override void StepB()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonArbalist);
    }
}
