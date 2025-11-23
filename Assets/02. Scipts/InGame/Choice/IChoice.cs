using UnityEngine;

public abstract class IChoice
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
