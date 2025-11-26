using System;
using UnityEngine;

public class PlayerHealth : SingletonBehaviour<PlayerHealth>
{
    private int _health = 100;
    private int _maxHealth = 100;
    public static event Action<int, int> OnHealthChanged;
    public static event Action OnDamaged;
    public static event Action OnDied;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    private void Start()
    {
        OnHealthChanged?.Invoke(Health, _maxHealth);
    }

    public int Health
    {
        get => _health;
        private set
        {
            _health = value;
            OnHealthChanged?.Invoke(_health, _maxHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        if (amount < 0) return;
        Health = Mathf.Max(Health - amount, 0);
        OnDamaged?.Invoke();
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0) return;
        Health = Mathf.Min(Health + amount, _maxHealth);
    }

    public void Die()
    {
        OnDied?.Invoke();
    }
}
