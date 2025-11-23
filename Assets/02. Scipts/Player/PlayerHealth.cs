using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _health = 100;
    private int _maxHealth = 100;
    public static event Action<int, int> OnHealthChanged;

    public void TakeDamage(int amount)
    {
        if (amount < 0) return;
        _health = Mathf.Max(_health - amount, 0);
        CheckDeath();
        OnHealthChanged?.Invoke(_health, _maxHealth);
    }

    public void Heal(int amount)
    {
        if (amount < 0) return;
        _health = Mathf.Min(_health + amount, _maxHealth);
        OnHealthChanged?.Invoke(_health, _maxHealth);
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
