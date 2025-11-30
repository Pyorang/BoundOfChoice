using UnityEngine;

public class Choice13 : ChoiceBase
{
    private static readonly float _ASpiritGainChance = 0.5f;
    private static readonly int _ASpiritGainAmount = 1;

    private static readonly int _BAdditionalDamageAmount = 5;
    private static readonly float _BAdditionalSpeed = 1f;
    private static readonly int _BAdditionalMaxHealth = 5;

    protected override void StepA()
    {
        if(Random.value <= _ASpiritGainChance)
        {
            SpiritManager.Instance.GetSpirit(_ASpiritGainAmount);
        }
    }

    protected override void StepB()
    {
        PlayerCombat.Instance.AdditionalPower += _BAdditionalDamageAmount;
        PlayerMovement.Instance.MoveSpeed += _BAdditionalSpeed;
        PlayerHealth.Instance.MaxHealth += _BAdditionalMaxHealth;
    }
}
