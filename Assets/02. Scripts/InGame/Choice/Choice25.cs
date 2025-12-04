using UnityEngine;

public class Choice25 : ChoiceBase
{
    private static readonly int MageAlreadyGainedGoldAmount = 70;

    protected override void StepLeft()
    {
        PlayerCombat.Instance.OpenCharacter(ECharacterType.Mage, MageAlreadyGainedGoldAmount);

        MonsterSpawner.Instance.SpawnMonsters(
            stackalloc EPoolType[]
            {
                EPoolType.SkeletonNecro,
                EPoolType.SkeletonNecro,
                EPoolType.SkeletonNecro
            }
        );
    }

    protected override void StepRight()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonNecro);
    }
}
