using UnityEngine;

public class Choice1 : ChoiceBase
{
    public override void Execute1()
    {
        ChoiceManager.Instance.SetChoice(2);
    }

    public override void Execute2()
    {
        ChoiceManager.Instance.SetChoice(2);
    }
}
