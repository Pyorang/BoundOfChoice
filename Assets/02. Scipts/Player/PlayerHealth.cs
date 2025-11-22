using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _health = 100;
    private int _maxHealth = 100;

    private InGameUIController _inGameUIController;

    private void Start()
    {
        _inGameUIController = FindAnyObjectByType<InGameUIController>();
    }

    public void TakeDamage(int amount)
    {
        if (amount < 0) return;
        _health = Mathf.Max(_health - amount, 0);
        CheckDeath();
        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        if (amount < 0) return;
        _health = Mathf.Min(_health + amount, _maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        _inGameUIController.OnUpdateHealthUI(_health, _maxHealth);
    }

    private void CheckDeath()
    {
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

    }
}
