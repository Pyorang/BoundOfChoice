using UnityEngine;

public class Choice2 : ChoiceBase
{
    public override void Execute1()
    {
        Debug.Log("Choice2 - Execute1 executed.");
        base.Execute1();
    }

    public override void Execute2()
    {
        Debug.Log("Choice2 - Execute2 executed.");
        base.Execute2();
    }
}
