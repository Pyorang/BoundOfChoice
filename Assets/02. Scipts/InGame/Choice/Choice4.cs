using UnityEngine;

public class Choice4 : ChoiceBase
{
    private static readonly float _damageChance = 0.5f;
    private static readonly int _damageAmount = 25;
    private static readonly int _healAmount = 35;

    protected override void StepA()
    {
        if(Random.value <= _damageChance)
        {
            PlayerHealth.Instance.TakeDamage(_damageAmount);
        }
        else
        {
            PlayerHealth.Instance.Heal(_healAmount);
        }
    }

    protected override void StepB()
    {

    }
}
