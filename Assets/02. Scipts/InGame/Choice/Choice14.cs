using UnityEngine;

public class Choice14 : ChoiceBase
{
    private static readonly int _AGoldAmount = 10;

    protected override void StepA()
    {
        GoldManager.Instance.GetGold(_AGoldAmount);
        Debug.Log("검사 스켈레톤 두 마리 소환");
    }

    protected override void StepB()
    {
        Debug.Log("검사 스켈레톤 한 마리 소환");
    }
}
