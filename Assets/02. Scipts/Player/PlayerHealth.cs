using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerStats _playerStats;

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
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
