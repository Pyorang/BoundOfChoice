using UnityEngine;

public class Choice1 : ChoiceBase
{
    private static readonly int NextChoiceID = 2;

    protected override void StepA()
    {

    }

    protected override void StepB()
    {

    }

    public override void ExecuteA()
    {
        ChoiceManager.Instance.SetChoice(NextChoiceID);
    }

    public override void ExecuteB()
    {
        ChoiceManager.Instance.SetChoice(NextChoiceID);
    }
}
