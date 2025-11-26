using UnityEngine;

public class Choice6 : ChoiceBase
{
    public override void Execute1()
    {
        GoldManager.Instance.GetGold(20);
        base.Execute1();
    }

    public override void Execute2()
    {
        PlayerHealth.Instance.Heal(5);
        base.Execute2();
    }
}
