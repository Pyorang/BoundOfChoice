using UnityEngine;

public class Choice4 : ChoiceBase
{
    private static readonly float DamageChance = 0.5f;
    private static readonly int DamageAmount = 25;
    private static readonly int HealAmount = 35;

    protected override void StepLeft()
    {
        if(Random.value <= DamageChance)
        {
            PlayerHealth.Instance.TakeDamage(DamageAmount);
        }
        else
        {
            PlayerHealth.Instance.Heal(HealAmount);
        }
    }

    protected override void StepRight()
    {

    }
}
