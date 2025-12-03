using UnityEngine;

public class Choice25 : ChoiceBase
{
    private static readonly int MageAlreadyGainedGoldAmount = 70;

    protected override void StepA()
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

    protected override void StepB()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.SkeletonNecro);
    }
}
