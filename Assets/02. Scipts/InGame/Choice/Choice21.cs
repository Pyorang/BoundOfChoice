using UnityEngine;

public class Choice21 : ChoiceBase
{
    private static readonly float _BActivateAllTrapChance = 0.25f;

    protected override void StepA()
    {
        KnightTrapManager.Instance.Activate();
    }

    protected override void StepB()
    {
        if(Random.value <= _BActivateAllTrapChance)
        {
            KnightTrapManager.Instance.ActivateAll();
        }
    }
}
