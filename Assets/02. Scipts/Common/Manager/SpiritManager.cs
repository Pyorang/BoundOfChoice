using System;
using UnityEngine;

public class SpiritManager : SingletonBehaviour<SpiritManager>
{
    private int _spiritPiece;
    [SerializeField] private int _maxSpiritPiece = 4;
    public static event Action<int, int> OnSpiritPieceChanged;
    public static event Action OnSpiritGained;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();

        _spiritPiece = 0;
    }

    public void GetSpiritPiece(int amount)
    {
        if (amount <= 0) return;
        _spiritPiece = Mathf.Min(_spiritPiece + amount, _maxSpiritPiece);

        OnSpiritGained?.Invoke();

        if(_spiritPiece == _maxSpiritPiece)
        {
            // NOTE : 게임 클리어 로직을 작성한다.
        }
    }

    public void RefreshSpiritUI()
    {
        OnSpiritPieceChanged?.Invoke(_spiritPiece, _maxSpiritPiece);
    }
}
