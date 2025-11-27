using UnityEngine;

public class Choice9 : ChoiceBase
{
    public override void Execute1()
    {
        KnightTrapManager.Instance.Deactivate();
        base.Execute1();
    }

    public override void Execute2()
    {
        float activateAllTrapChance = 0.25f;

        if (Random.value < activateAllTrapChance)
        {
            KnightTrapManager.Instance.DeActivateAll();
        }
        base.Execute2();
    }
}
