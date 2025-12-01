using UnityEngine;

public class Choice30 : ChoiceBase
{
    private static readonly float MoveSpeedResetChance = 0.3f;
    private static readonly float AdditionalDamageResetChance = 0.6f;

    protected override void StepA()
    {
        if(Random.value <= MoveSpeedResetChance)
        {
            PlayerMovement.Instance.ResetSpeed();
        }
    }

    protected override void StepB()
    {
        if (Random.value <= AdditionalDamageResetChance)
        {
            PlayerCombat.Instance.AdditionalPower = 0;
        }
    }
}
