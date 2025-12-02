using UnityEngine;

public class Choice6 : ChoiceBase
{
    private static readonly float ADamageChance = 0.2f;
    private static readonly float ADamageRatio = 0.4f;

    private static readonly float BDamageChance = 0.8f;
    private static readonly float BDamageRatio = 0.1f;

    protected override void StepA()
    {
        if (Random.value <= ADamageChance)
        {
            PlayerHealth.Instance.TakeDamage((int)(PlayerHealth.Instance.Health * (ADamageRatio)));
        }
    }

    protected override void StepB()
    {
        if (Random.value <= BDamageChance)
        {
            PlayerHealth.Instance.TakeDamage((int)(PlayerHealth.Instance.Health * (BDamageRatio)));
        }
    }
}