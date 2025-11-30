using UnityEngine;

public class Choice28 : ChoiceBase
{
    private static readonly int _AAdditionalDamageAmount = 2;

    private static readonly int _BAdditionalDamageAmount = 4;

    protected override void StepA()
    {
        PlayerCombat.Instance.AdditionalPower += _AAdditionalDamageAmount;
    }

    protected override void StepB()
    {
        PlayerCombat.Instance.AdditionalPower += Random.Range(0, _BAdditionalDamageAmount + 1);
    }
}
