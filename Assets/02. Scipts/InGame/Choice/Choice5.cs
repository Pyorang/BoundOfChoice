using UnityEngine;

public class Choice5 : ChoiceBase
{
    private static readonly int AHealAmount = 15;
    private static readonly int AManaRegenerateAmount = 20;

    private static readonly int BHealAmount = 10;
    private static readonly int BManaRegenerateAmount = 40;

    protected override void StepA()
    {
        PlayerHealth.Instance.Heal(AHealAmount);
        PlayerMana.Instance.RegenerateMana(AManaRegenerateAmount);
    }

    protected override void StepB()
    {
        PlayerHealth.Instance.Heal(BHealAmount);
        PlayerMana.Instance.RegenerateMana(BManaRegenerateAmount);
    }
}