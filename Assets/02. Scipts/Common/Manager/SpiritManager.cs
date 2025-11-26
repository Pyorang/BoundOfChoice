using System;
using UnityEngine;

public class SpiritManager : SingletonBehaviour<SpiritManager>
{
    private int _spiritPiece;
    private const int MaxSpiritPiece = 4;
    public static event Action<int, int> OnSpiritPieceChanged;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();

        SpiritPiece = 0;
    }
    public int SpiritPiece
    {
        get => _spiritPiece;
        private set
        {
            _spiritPiece = value;
            OnSpiritPieceChanged?.Invoke(_spiritPiece, MaxSpiritPiece);
        }
    }

    public void GetSpiritPiece(int amount)
    {
        if (amount < 0) return;
        SpiritPiece = Mathf.Min(SpiritPiece + amount, MaxSpiritPiece);
    }
}
