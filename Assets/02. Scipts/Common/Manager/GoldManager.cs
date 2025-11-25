using System;
using UnityEngine;

public class GoldManager : SingletonBehaviour<GoldManager>
{
    private int _gold;
    private const int MaxGold = 9999;
    public static event Action<int> OnGoldChanged;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();

        Gold = 0;
    }
    public int Gold
    {
        get => _gold;
        private set
        {
            _gold = value;
            OnGoldChanged?.Invoke(_gold);
        }
    }

    public void GetGold(int amount)
    {
        if (amount < 0) return;
        Gold = Mathf.Min(Gold + amount, MaxGold);
    }

    public bool UseGold(int amount)
    {
        if (amount < 0 || Gold < amount) return false;

        Gold -= amount;
        return true;
    }
}
