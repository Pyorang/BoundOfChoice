using System;
using System.Collections;
using UnityEngine;

public class SpiritManager : SingletonBehaviour<SpiritManager>
{
    private int _currentSpiritCount;
    public static event Action<int, int> OnSpiritCountValueChanged;
    public static event Action OnSpiritGained;
    public static event Action OnSpiritCompleted;

    [Header("승리 조건")]
    [Space]
    [SerializeField] private int _maxSpiritCount;

    [Header("소환 딜레이")]
    [Space]
    [SerializeField] private float _spiritSpawnDelay = 0.5f;
    private Coroutine _spiritSpawnCoroutine;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();

        _currentSpiritCount = 0;
    }

    public void GetSpirit(int amount)
    {
        if (amount <= 0) return;
        if (_spiritSpawnCoroutine != null)
        {
            StopCoroutine(_spiritSpawnCoroutine);
        }
        _spiritSpawnCoroutine = StartCoroutine(SpawnSpirit(amount));
    }

    private IEnumerator SpawnSpirit(int amount)
    {
        WaitForSeconds delay = new WaitForSeconds(_spiritSpawnDelay);
        for (int i = 0; i < amount; i++)
        {
            AudioManager.Instance.Play(AudioType.SFX, "Spirit");
            _currentSpiritCount = Mathf.Min(_currentSpiritCount + 1, _maxSpiritCount);
            OnSpiritGained?.Invoke();
            yield return delay;
        }
        _spiritSpawnCoroutine = null;
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
