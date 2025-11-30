using UnityEngine;

public class Choice16 : ChoiceBase
{
    private static readonly int _AActivateTrapAmount = 2;

    protected override void StepA()
    {
        for(int i = 0; i < _AActivateTrapAmount; i++)
        {
            KnightTrapManager.Instance.Activate();
        }

        Debug.Log("검사 스켈레톤 한 마리 소환");
    }

    protected override void StepB()
    {
        Debug.Log("엘리트 스켈레톤 한 마리 소환");
    }
}
