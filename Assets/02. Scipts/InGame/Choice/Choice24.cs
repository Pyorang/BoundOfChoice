using UnityEngine;

public class Choice24 : ChoiceBase
{
    private static readonly int ArcherAlreadyGainedGoldAmount = 50;

    protected override void StepA()
    {
        PlayerCombat.Instance.OpenCharacter(ECharacterType.Archer, ArcherAlreadyGainedGoldAmount);
        Debug.Log("궁수 스켈레톤 4마리 소환");
    }

    protected override void StepB()
    {
        Debug.Log("궁수 스켈레톤 1마리 소환");
    }
}
