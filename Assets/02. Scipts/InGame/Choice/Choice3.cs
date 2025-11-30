using UnityEngine;

public class Choice3 : ChoiceBase
{
    private static readonly float _deathChance = 0.1f;

    private static readonly float _damageChance = 0.9f;
    private static readonly int _damageAmount = 30;

    protected override void StepA()
    {
        if (Random.value <= _deathChance)
        {
            PlayerHealth.Instance.Die();
        }

        GetNewChoice();
    }

    protected override void StepB()
    {
        if (Random.value <= _damageChance)
        {
            PlayerHealth.Instance.TakeDamage(_damageAmount);
        }

        GetNewChoice();
    }
}
