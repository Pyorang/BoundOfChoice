using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    private int _mana = 100;
    private int _maxMana = 100;

    private InGameUIController _inGameUIController;

    private void Start()
    {
        _inGameUIController = FindAnyObjectByType<InGameUIController>();
    }

    public bool TryUseMana(int amount)
    {
        if (amount < 0) return false;
        if (_mana < amount) return false;
        _mana = Mathf.Max(_mana - amount, 0);
        UpdateManaUI();
        return true;
    }

    public void RegenerateMana(int amount)
    {
        if (amount < 0) return;
        _mana = Mathf.Min(_mana + amount, _maxMana);
        UpdateManaUI();
    }

    private void UpdateManaUI()
    {
        _inGameUIController.OnUpdateManaUI(_mana, _maxMana);
    }
}
