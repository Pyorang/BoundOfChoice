using UnityEngine;

public class Choice1 : ChoiceBase
{
    private static readonly int NextChoiceID = 2;

    protected override void StepLeft()
    {

    }

    protected override void StepRight()
    {

    }

    public override void ExecuteLeft()
    {
        ChoiceManager.Instance.SetChoice(NextChoiceID);
    }

    public override void ExecuteRight()
    {
        ChoiceManager.Instance.SetChoice(NextChoiceID);
    }
}
