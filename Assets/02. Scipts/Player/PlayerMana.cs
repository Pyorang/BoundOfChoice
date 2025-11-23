using System;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    private int _mana = 100;
    private int _maxMana = 100;
    public static event Action<int, int> OnManaChanged;

    public bool TryUseMana(int amount)
    {
        if (amount < 0) return false;
        if (_mana < amount) return false;
        _mana = Mathf.Max(_mana - amount, 0);
        OnManaChanged?.Invoke(_mana, _maxMana);
        return true;
    }

    public void RegenerateMana(int amount)
    {
        if (amount < 0) return;
        _mana = Mathf.Min(_mana + amount, _maxMana);
        OnManaChanged?.Invoke(_mana, _maxMana);
    }
}
