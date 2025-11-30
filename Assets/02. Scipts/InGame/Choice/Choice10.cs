using UnityEngine;

public class Choice10 : ChoiceBase
{
    private static readonly int _AGoldAmount = 10;

    private static readonly int _BHealAmount = 5;

    protected override void StepA()
    {
        GoldManager.Instance.GetGold(_AGoldAmount);
    }

    protected override void StepB()
    {
        PlayerHealth.Instance.Heal(_BHealAmount);
    }
}
