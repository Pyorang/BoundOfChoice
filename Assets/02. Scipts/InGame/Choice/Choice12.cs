using UnityEngine;

public class Choice12 : ChoiceBase
{
    private static readonly float _AEscapeChance = 0.005f;

    private static readonly float _BSpiritGainChance = 0.05f;
    private static readonly int _BSpiritGainAmount = 1;

    protected override void StepA()
    {
        if(Random.value <= _AEscapeChance)
        {
            SpiritManager.Instance.FillRemainingSpirit();
        }
    }

    protected override void StepB()
    {
        if(Random.value <= _BSpiritGainChance)
        {
            SpiritManager.Instance.GetSpirit(_BSpiritGainAmount);
        }
    }
}
