using UnityEngine;

public class Choice29 : ChoiceBase
{
    private static readonly int AAdditionalSpeedAmount = 1;

    private static readonly int BAdditionalSpeedAmount = 2;

    protected override void StepA()
    {
        PlayerMovement.Instance.MoveSpeed += AAdditionalSpeedAmount;
    }

    protected override void StepB()
    {
        PlayerMovement.Instance.MoveSpeed += Random.Range(0, BAdditionalSpeedAmount);
    }
}
