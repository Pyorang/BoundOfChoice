using UnityEngine;

public class Choice22 : ChoiceBase
{
    private static readonly float BDeActivateAllTrapChance = 0.25f;

    protected override void StepLeft()
    {
        KnightTrapManager.Instance.Deactivate();
    }

    protected override void StepRight()
    {
        if (Random.value <= BDeActivateAllTrapChance)
        {
            KnightTrapManager.Instance.DeActivateAll();
        }
    }
}
