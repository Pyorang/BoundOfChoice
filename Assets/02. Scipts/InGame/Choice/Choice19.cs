using UnityEngine;

public class Choice19 : ChoiceBase
{
    private static readonly int _BDamageAmount = 25;

    protected override void StepA()
    {
        Debug.Log("검사 스켈레톤 한 마리와 궁수 스켈레톤 두 마리를 소환");
    }

    protected override void StepB()
    {
        PlayerHealth.Instance.TakeDamage(_BDamageAmount);
    }
}
