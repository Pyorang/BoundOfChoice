using UnityEngine;

public abstract class ChoiceBase
{
    public virtual void Execute1()
    {
        ChoiceManager.Instance.GetNewChoice();
    }

    public virtual void Execute2()
    {
        ChoiceManager.Instance.GetNewChoice();
    }
}
