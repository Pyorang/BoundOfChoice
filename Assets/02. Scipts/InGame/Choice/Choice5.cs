using UnityEngine;

public class Choice5 : ChoiceBase
{
    private static readonly int _AHealAmount = 15;
    private static readonly int _AManaRegenerateAmount = 20;

    private static readonly int _BHealAmount = 10;
    private static readonly int _BManaRegenerateAmount = 40;

    protected override void StepA()
    {
        PlayerHealth.Instance.Heal(_AHealAmount);
        PlayerMana.Instance.RegenerateMana(_AManaRegenerateAmount);
    }

    protected override void StepB()
    {
        PlayerHealth.Instance.Heal(_BHealAmount);
        PlayerMana.Instance.RegenerateMana(_BManaRegenerateAmount);
    }
}