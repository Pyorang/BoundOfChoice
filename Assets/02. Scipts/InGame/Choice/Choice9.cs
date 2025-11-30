using UnityEngine;

public class Choice9 : ChoiceBase
{
    private static readonly int _AHealAmount = 10;
    private static readonly int _AGoldAmount = 20;

    private static readonly int _BHealAmount = 20;

    protected override void StepA()
    {
        PlayerHealth.Instance.Heal(_AHealAmount);
        GoldManager.Instance.GetGold(_AGoldAmount);
    }

    protected override void StepB()
    {
        PlayerHealth.Instance.Heal(_BHealAmount);
    }
}
