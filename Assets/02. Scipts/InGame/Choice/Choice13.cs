using UnityEngine;

public class Choice13 : ChoiceBase
{
    private static readonly float ASpiritGainChance = 0.5f;
    private static readonly int ASpiritGainAmount = 1;

    private static readonly int BAdditionalDamageAmount = 5;
    private static readonly float BAdditionalSpeed = 1f;
    private static readonly int BAdditionalMaxHealth = 5;

    protected override void StepA()
    {
        if(Random.value <= ASpiritGainChance)
        {
            SpiritManager.Instance.GetSpirit(ASpiritGainAmount);
        }
    }

    protected override void StepB()
    {
        PlayerCombat.Instance.AdditionalPower += BAdditionalDamageAmount;
        PlayerMovement.Instance.MoveSpeed += BAdditionalSpeed;
        PlayerHealth.Instance.MaxHealth += BAdditionalMaxHealth;
    }
}
