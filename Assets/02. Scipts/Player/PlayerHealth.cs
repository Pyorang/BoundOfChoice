using UnityEngine;

public class PlayerHealth : MonoBehaviour
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

    public void TakeDamage(int amount)
    {
        _playerStats.TakeDamage(amount);
        CheckDeath();
        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        _playerStats.Heal(amount);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        _inGameUIController.OnUpdateHealthUI(_playerStats.Health, _playerStats.MaxHealth);
    }

    private void CheckDeath()
    {
        if (_playerStats.Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

    }
}
