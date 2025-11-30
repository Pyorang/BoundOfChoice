using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Choice23 : ChoiceBase
{
    private static readonly int _executeCount = 3;
    private static readonly float _effectDuration = 1.0f;
    private static readonly float _normalRadius = 7.0f;
    private static readonly float _narrowedRadius = 4.0f;
    private static readonly float _widenedRadius = 13.0f;

    protected override void StepA()
    {
        _executeACount = _executeCount;

        if(Random.value >= 0.5f)
        {
            PlayerCombat.Instance.ChangePlayerVision(_effectDuration, _narrowedRadius);
        }
        else
        {
            PlayerCombat.Instance.ChangePlayerVision(_effectDuration, _widenedRadius);
        }
    }

    protected override void StepB()
    {
        
    }

    protected override void ExecuteRemainingA()
    {
        if (_executeACount == 0)
        {
            PlayerCombat.Instance.ChangePlayerVision(_effectDuration, _normalRadius);
        }
    }
}
