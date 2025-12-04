using UnityEngine;

public class Choice7 : ChoiceBase
{
    private static readonly int ExecuteCount = 5;
    private static readonly int DamageAmount = 10;

    protected override void StepLeft()
    {
        _executeLeftCount = ExecuteCount;
        ChoiceManager.OnRightLeverInteracted += GiveDamage;
    }

    protected override void StepRight()
    {
        _executeRightCount = ExecuteCount;
        ChoiceManager.OnLeftLeverInteracted += GiveDamage;
    }

    protected override void ExecuteLeftRemaining()
    {
        if(_executeLeftCount == 0)
        {
            ChoiceManager.OnRightLeverInteracted -= GiveDamage;
        }
    }

    protected override void ExecuteRightRemaining()
    {
        if (_executeRightCount == 0)
        {
            ChoiceManager.OnLeftLeverInteracted -= GiveDamage;
        }
    }

    public void GiveDamage()
    {
        PlayerHealth.Instance.TakeDamage(DamageAmount);
    }
}
