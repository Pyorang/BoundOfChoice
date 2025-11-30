using UnityEngine;

public class Choice8 : ChoiceBase
{
    private static readonly int _executeCount = 10;
    private static readonly int _ADamageAmount = 5;

    private static readonly int _BDamageAmount = 40;

    protected override void StepA()
    {
       _executeACount = _executeCount;
        ChoiceManager.OnLeftLeverInteracted += GiveDamage;
        ChoiceManager.OnRightLeverInteracted += GiveDamage;
    }

    protected override void StepB()
    {
        PlayerHealth.Instance.TakeDamage(_BDamageAmount);
    }

    protected override void ExecuteRemainingA()
    {
        if (_executeACount == 0)
        {
            ChoiceManager.OnLeftLeverInteracted -= GiveDamage;
            ChoiceManager.OnRightLeverInteracted -= GiveDamage;
        }
    }

    private void GiveDamage()
    {
        PlayerHealth.Instance.TakeDamage(_ADamageAmount);
    }
    
    ~Choice8()
    {
        ChoiceManager.OnLeftLeverInteracted -= GiveDamage;
        ChoiceManager.OnRightLeverInteracted -= GiveDamage;
    }
}
