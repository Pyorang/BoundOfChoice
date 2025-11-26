using UnityEngine;

public class Choice7 : ChoiceBase
{
    public override void Execute1()
    {
        SpiritManager.Instance.GetSpiritPiece(1);
        base.Execute1();
    }

    public override void Execute2()
    {
        const float GainSpiritChance = 0.45f;
        const int SpiritPieceAmount = 2;

        if (Random.value < GainSpiritChance)
        {
            SpiritManager.Instance.GetSpiritPiece(SpiritPieceAmount);
        }
        base.Execute2();
    }
}
