using UnityEngine;

public class Choice29 : ChoiceBase
{
    private static readonly int AAdditionalSpeedAmount = 1;

    private static readonly int BAdditionalSpeedAmount = 2;

    protected override void StepLeft()
    {
        PlayerMovement.Instance.AdditionalMoveSpeed += AAdditionalSpeedAmount;
    }

    protected override void StepRight()
    {
        PlayerMovement.Instance.AdditionalMoveSpeed += Random.Range(0, BAdditionalSpeedAmount + 1);
    }
}
