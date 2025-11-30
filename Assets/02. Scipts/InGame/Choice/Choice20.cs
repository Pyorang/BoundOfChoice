using UnityEngine;

public class Choice20 : ChoiceBase
{
    protected override void StepA()
    {
        Debug.Log("명예를 아는 자를 소환");
    }

    protected override void StepB()
    {
        Debug.Log("비열한 패거리를 소환");
    }
}
