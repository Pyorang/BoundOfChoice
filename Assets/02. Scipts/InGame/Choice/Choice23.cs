using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Choice23 : ChoiceBase
{
    private static readonly int ExecuteCount = 3;
    private static readonly float EffectDuration = 1.0f;
    private static readonly float NormalRadius = 7.0f;
    private static readonly float NarrowedRadius = 4.0f;
    private static readonly float WidenedRadius = 13.0f;

    protected override void StepA()
    {
        _executeACount = ExecuteCount;

        if(Random.value >= 0.5f)
        {
            PlayerCombat.Instance.ChangePlayerVision(EffectDuration, NarrowedRadius);
        }
        else
        {
            PlayerCombat.Instance.ChangePlayerVision(EffectDuration, WidenedRadius);
        }
    }

    protected override void StepB()
    {
        
    }

    protected override void ExecuteRemainingA()
    {
        if (_executeACount == 0)
        {
            PlayerCombat.Instance.ChangePlayerVision(EffectDuration, NormalRadius);
        }
    }
}
