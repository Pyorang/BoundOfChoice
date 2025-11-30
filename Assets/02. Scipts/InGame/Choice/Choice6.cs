using UnityEngine;

public class Choice6 : ChoiceBase
{
    private static readonly float _ADamageChance = 0.2f;
    private static readonly float _ADamageRatio = 0.4f;

    private static readonly float _BDamageChance = 0.8f;
    private static readonly float _BDamageRatio = 0.1f;

    protected override void StepA()
    {
        if (Random.value <= _ADamageChance)
        {
            PlayerHealth.Instance.TakeDamage((int)(PlayerHealth.Instance.Health * (_ADamageRatio)));
        }
    }

    protected override void StepB()
    {
        if (Random.value <= _BDamageChance)
        {
            PlayerHealth.Instance.TakeDamage((int)(PlayerHealth.Instance.Health * (_BDamageRatio)));
        }
    }
}