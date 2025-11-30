using UnityEngine;

public class Choice7 : ChoiceBase
{
    private static readonly int _executeCount = 5;
    private static readonly int _damageAmount = 10;

    protected override void StepA()
    {
        _executeACount = _executeCount;
        ChoiceManager.OnRightLeverInteracted += GiveDamage;
    }

    protected override void StepB()
    {
        _executeBCount = _executeCount;
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
        PlayerHealth.Instance.TakeDamage(_damageAmount);
    }

    ~Choice7()
    {
        ChoiceManager.OnLeftLeverInteracted -= GiveDamage;
        ChoiceManager.OnRightLeverInteracted -= GiveDamage;
    }
}
