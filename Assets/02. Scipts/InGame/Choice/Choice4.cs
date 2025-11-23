using UnityEngine;

public class Choice4 : IChoice
{
    public override void Execute1()
    {
        Debug.Log("Choice4 - Execute1 executed.");
        base.Execute1();
    }

    public override void Execute2()
    {
        Debug.Log("Choice4 - Execute2 executed.");
        base.Execute2();
    }
}
