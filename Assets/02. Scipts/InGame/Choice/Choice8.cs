using UnityEngine;

public class Choice8 : ChoiceBase
{
    public override void Execute1()
    {
        KnightTrapManager.Instance.Activate();
        base.Execute1();
    }

    public override void Execute2()
    {
        float activateAllTrapChance = 0.25f;

        if (Random.value < activateAllTrapChance)
        {
            KnightTrapManager.Instance.ActivateAll();
        }
        base.Execute2();
    }
}
