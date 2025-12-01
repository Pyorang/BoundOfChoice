using UnityEngine;

public class Choice12 : ChoiceBase
{
    private static readonly float AEscapeChance = 0.005f;

    private static readonly float BSpiritGainChance = 0.05f;
    private static readonly int BSpiritGainAmount = 1;

    protected override void StepA()
    {
        if(Random.value <= AEscapeChance)
        {
            SpiritManager.Instance.FillRemainingSpirit();
        }
    }

    protected override void StepB()
    {
        if(Random.value <= BSpiritGainChance)
        {
            SpiritManager.Instance.GetSpirit(BSpiritGainAmount);
        }
    }
}
