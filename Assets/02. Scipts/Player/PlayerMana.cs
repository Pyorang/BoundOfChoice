using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    private PlayerStats _playerStats;
    private InGameUIController _inGameUIController;

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        _inGameUIController = FindAnyObjectByType<InGameUIController>();
    }

    public bool TryUseMana(int amount)
    {
        if (_playerStats.Mana < amount) return false;
        _playerStats.UseMana(amount);
        UpdateManaUI();
        return true;
    }

    public void RegenerateMana(int amount)
    {
        _playerStats.RegenerateMana(amount);
        UpdateManaUI();
    }

    private void UpdateManaUI()
    {
        _inGameUIController.OnUpdateManaUI(_playerStats.Mana, _playerStats.MaxMana);
    }
}
