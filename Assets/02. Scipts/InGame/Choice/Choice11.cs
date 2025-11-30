using UnityEngine;

public class Choice11 : ChoiceBase
{
    private static readonly float _ASpiritGainChance = 0.45f;
    private static readonly int _ASpiritGainAmount = 2;

    private static readonly int _BSpiritGainAmount = 1;

    protected override void StepA()
    {
        if(Random.value <= _ASpiritGainChance)
        {
            SpiritManager.Instance.GetSpirit(_ASpiritGainAmount);
        }
    }

    protected override void StepB()
    {
        SpiritManager.Instance.GetSpirit(_BSpiritGainAmount);
    }
}
