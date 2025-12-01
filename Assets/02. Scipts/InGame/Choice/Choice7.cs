using UnityEngine;

public class Choice7 : ChoiceBase
{
    private static readonly int ExecuteCount = 5;
    private static readonly int DamageAmount = 10;

    protected override void StepA()
    {
        _executeACount = ExecuteCount;
        ChoiceManager.OnRightLeverInteracted += GiveDamage;
    }

    protected override void StepB()
    {
        _executeBCount = ExecuteCount;
        ChoiceManager.OnLeftLeverInteracted += GiveDamage;
    }

    protected override void ExecuteRemainingA()
    {
        if(_executeACount == 0)
        {
            ChoiceManager.OnRightLeverInteracted -= GiveDamage;
        }
    }

    protected override void ExecuteRemainingB()
    {
        if (_executeBCount == 0)
        {
            ChoiceManager.OnLeftLeverInteracted -= GiveDamage;
        }
    }

    public void GiveDamage()
    {
        PlayerHealth.Instance.TakeDamage(DamageAmount);
    }
}
