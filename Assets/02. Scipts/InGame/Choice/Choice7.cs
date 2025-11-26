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
        if (Random.value < 0.55f) return;
        SpiritManager.Instance.GetSpiritPiece(2);
        base.Execute2();
    }
}
