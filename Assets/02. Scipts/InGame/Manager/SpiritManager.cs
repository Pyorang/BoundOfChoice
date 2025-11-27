using System;
using UnityEngine;

public class SpiritManager : SingletonBehaviour<SpiritManager>
{
    private int _spiritPiece;
    [SerializeField] private int _maxSpiritPiece;
    public static event Action<int, int> OnSpiritPieceChanged;
    public static event Action OnSpiritGained;
    public static event Action OnSpiritCompleted;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();

        _spiritPiece = 0;
    }

    public void GetSpiritPiece(int amount)
    {
        if (amount <= 0) return;
        AudioManager.Instance.Play(AudioType.SFX, "Spirit");
        _spiritPiece = Mathf.Min(_spiritPiece + amount, _maxSpiritPiece);
        OnSpiritGained?.Invoke();
    }

    public void RefreshSpiritUI()
    {
        OnSpiritPieceChanged?.Invoke(_spiritPiece, _maxSpiritPiece);

        if (_spiritPiece == _maxSpiritPiece)
        {
            OnSpiritCompleted?.Invoke();
        }
    }
}
