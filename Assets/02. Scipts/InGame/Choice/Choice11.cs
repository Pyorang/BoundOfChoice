using UnityEngine;

public class Choice11 : ChoiceBase
{
    private static readonly float ASpiritGainChance = 0.45f;
    private static readonly int ASpiritGainAmount = 2;

    private static readonly int BSpiritGainAmount = 1;

    protected override void StepLeft()
    {
        if(Random.value <= ASpiritGainChance)
        {
            SpiritManager.Instance.GetSpirit(ASpiritGainAmount);
        }
    }

    protected override void StepRight()
    {
        SpiritManager.Instance.GetSpirit(BSpiritGainAmount);
    }
}
