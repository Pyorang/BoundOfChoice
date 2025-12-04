using UnityEngine;

public class Choice3 : ChoiceBase
{
    private static readonly float DeathChance = 0.1f;

    private static readonly float DamageChance = 0.9f;
    private static readonly int DamageAmount = 30;

    protected override void StepLeft()
    {
        if (Random.value <= DeathChance)
        {
            PlayerHealth.Instance.Die();
        }

        GetNewChoice();
    }

    protected override void StepRight()
    {
        if (Random.value <= DamageChance)
        {
            PlayerHealth.Instance.TakeDamage(DamageAmount);
        }

        GetNewChoice();
    }
}
