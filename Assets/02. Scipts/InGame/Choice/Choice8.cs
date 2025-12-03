using UnityEngine;

public class Choice8 : ChoiceBase
{
    private static readonly int ExecuteCount = 10;
    private static readonly int ADamageAmount = 5;

    private static readonly int BDamageAmount = 40;

    protected override void StepLeft()
    {
       _executeLeftCount = ExecuteCount;
        ChoiceManager.OnLeftLeverInteracted += GiveDamage;
        ChoiceManager.OnRightLeverInteracted += GiveDamage;
    }

    protected override void StepRight()
    {
        PlayerHealth.Instance.TakeDamage(BDamageAmount);
    }

    protected override void ExecuteLeftRemaining()
    {
        if (_executeLeftCount == 0)
        {
            ChoiceManager.OnLeftLeverInteracted -= GiveDamage;
            ChoiceManager.OnRightLeverInteracted -= GiveDamage;
        }
    }

    private void GiveDamage()
    {
        PlayerHealth.Instance.TakeDamage(ADamageAmount);
    }
   
}
