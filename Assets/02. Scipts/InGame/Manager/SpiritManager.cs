using System;
using UnityEngine;

public class SpiritManager : SingletonBehaviour<SpiritManager>
{
    private int _currentSpiritCount;
    [SerializeField] private int _maxSpiritCount;
    public static event Action<int, int> OnSpiritCountValueChanged;
    public static event Action OnSpiritGained;
    public static event Action OnSpiritCompleted;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();

        _currentSpiritCount = 0;
    }

    public void GetSpirit(int amount)
    {
        if (amount <= 0) return;
        AudioManager.Instance.Play(AudioType.SFX, "Spirit");
        _currentSpiritCount = Mathf.Min(_currentSpiritCount + amount, _maxSpiritCount);
        OnSpiritGained?.Invoke();
    }

    public void FillRemainingSpirit()
    {
        GetSpirit(_maxSpiritCount - _currentSpiritCount);
    }

    public void RefreshSpiritUI()
    {
        OnSpiritCountValueChanged?.Invoke(_currentSpiritCount, _maxSpiritCount);

        if (_currentSpiritCount == _maxSpiritCount)
        {
            OnSpiritCompleted?.Invoke();
        }
    }
}
