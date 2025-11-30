using UnityEngine;

public class Choice29 : ChoiceBase
{
    private static readonly float _AAdditionalSpeedAmount = 1.0f;

    private static readonly float _BAdditionalSpeedAmount = 2.0f;

    protected override void StepA()
    {
        PlayerMovement.Instance.MoveSpeed += _AAdditionalSpeedAmount;
    }

    protected override void StepB()
    {
        PlayerMovement.Instance.MoveSpeed += Random.Range(0, _BAdditionalSpeedAmount);
    }
}
