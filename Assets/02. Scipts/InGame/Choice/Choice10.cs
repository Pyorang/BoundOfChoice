using UnityEngine;

public class Choice10 : ChoiceBase
{
    private static readonly int AGoldAmount = 10;

    private static readonly int BHealAmount = 5;

    protected override void StepA()
    {
        GoldManager.Instance.GetGold(AGoldAmount);
    }

    protected override void StepB()
    {
        PlayerHealth.Instance.Heal(BHealAmount);
    }
}
