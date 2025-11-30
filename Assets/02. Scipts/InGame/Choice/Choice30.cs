using UnityEngine;

public class Choice30 : ChoiceBase
{
    private static readonly float _moveSpeedResetChance = 0.3f;
    private static readonly float _additionalDamageResetChance = 0.6f;

    protected override void StepA()
    {
        if(Random.value <= _moveSpeedResetChance)
        {
            PlayerMovement.Instance.ResetSpeed();
        }
    }

    protected override void StepB()
    {
        if (Random.value <= _additionalDamageResetChance)
        {
            PlayerCombat.Instance.AdditionalPower = 0;
        }
    }
}
