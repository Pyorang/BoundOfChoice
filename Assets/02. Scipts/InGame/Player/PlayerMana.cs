using System;
using UnityEngine;

public class PlayerMana : SingletonBehaviour<PlayerMana>
{
    private int _mana = 100;
    private int _maxMana = 100;
    public static event Action<int, int> OnManaChanged;

    public int Mana
    {
        get => _mana;
        private set
        {
            _mana = value;
            OnManaChanged?.Invoke(_mana, _maxMana);
        }
    }

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    private void Start()
    {
        OnManaChanged?.Invoke(Mana, _maxMana);
    }

    public bool TryUseMana(int amount)
    {
        if (amount < 0) return false;
        if (Mana < amount) return false;
        Mana = Mathf.Max(Mana - amount, 0);
        return true;
    }

    public void RegenerateMana(int amount)
    {
        if (amount < 0) return;
        Mana = Mathf.Min(Mana + amount, _maxMana);
    }
}
