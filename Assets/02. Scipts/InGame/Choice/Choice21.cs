using UnityEngine;

public class Choice21 : ChoiceBase
{
    private static readonly float BActivateAllTrapChance = 0.25f;

    protected override void StepLeft()
    {
        KnightTrapManager.Instance.Activate();
    }

    protected override void StepRight()
    {
        if(Random.value <= BActivateAllTrapChance)
        {
            KnightTrapManager.Instance.ActivateAll();
        }
    }
}
