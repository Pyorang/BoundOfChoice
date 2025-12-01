using UnityEngine;

public class Choice29 : ChoiceBase
{
    private static readonly float AAdditionalSpeedAmount = 1.0f;

    private static readonly float BAdditionalSpeedAmount = 2.0f;

    protected override void StepA()
    {
        PlayerMovement.Instance.MoveSpeed += AAdditionalSpeedAmount;
    }

    protected override void StepB()
    {
        PlayerMovement.Instance.MoveSpeed += Random.Range(0, BAdditionalSpeedAmount);
    }
}
