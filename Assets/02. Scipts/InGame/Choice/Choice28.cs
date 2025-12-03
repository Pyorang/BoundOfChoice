using UnityEngine;

public class Choice28 : ChoiceBase
{
    private static readonly int AAdditionalDamageAmount = 2;

    private static readonly int BAdditionalDamageAmount = 4;

    protected override void StepLeft()
    {
        PlayerCombat.Instance.AdditionalPower += AAdditionalDamageAmount;
    }

    protected override void StepRight()
    {
        PlayerCombat.Instance.AdditionalPower += Random.Range(0, BAdditionalDamageAmount + 1);
    }
}
