using UnityEngine;

public class Choice28 : ChoiceBase
{
    private static readonly int AAdditionalDamageAmount = 2;

    private static readonly int BAdditionalDamageAmount = 4;

    protected override void StepA()
    {
        PlayerCombat.Instance.AdditionalPower += AAdditionalDamageAmount;
    }

    protected override void StepB()
    {
        PlayerCombat.Instance.AdditionalPower += Random.Range(0, BAdditionalDamageAmount + 1);
    }
}
