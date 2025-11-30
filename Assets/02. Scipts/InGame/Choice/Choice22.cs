using UnityEngine;

public class Choice22 : ChoiceBase
{
    private static readonly float _BDeActivateAllTrapChance = 0.25f;

    protected override void StepA()
    {
        KnightTrapManager.Instance.Deactivate();
    }

    protected override void StepB()
    {
        if (Random.value <= _BDeActivateAllTrapChance)
        {
            KnightTrapManager.Instance.DeActivateAll();
        }
    }
}
