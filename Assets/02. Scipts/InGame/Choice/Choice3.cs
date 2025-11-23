using UnityEngine;

public class Choice3 : ChoiceBase
{
    public override void Execute1()
    {
        Debug.Log("Choice3 - Execute1 executed.");
        base.Execute1();
    }

    public override void Execute2()
    {
        Debug.Log("Choice3 - Execute2 executed.");
        base.Execute2();
    }
}