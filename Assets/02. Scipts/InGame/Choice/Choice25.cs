using UnityEngine;

public class Choice25 : ChoiceBase
{
    private static readonly int _mageAlreadyGainedGoldAmount = 70;

    protected override void StepA()
    {
        PlayerCombat.Instance.OpenCharacter(ECharacterType.Mage, _mageAlreadyGainedGoldAmount);
        Debug.Log("네크로 스켈레톤 3마리 소환");
    }

    protected override void StepB()
    {
        Debug.Log("네크로 스켈레톤 1마리 소환");
    }
}
