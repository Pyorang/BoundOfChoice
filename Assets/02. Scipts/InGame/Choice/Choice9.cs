using UnityEngine;

public class Choice9 : ChoiceBase
{
    private static readonly int AHealAmount = 10;
    private static readonly int AGoldAmount = 20;

    private static readonly int BHealAmount = 20;

    protected override void StepA()
    {
        PlayerHealth.Instance.Heal(AHealAmount);
        GoldManager.Instance.GetGold(AGoldAmount);
    }

    protected override void StepB()
    {
        PlayerHealth.Instance.Heal(BHealAmount);
    }
}
