using System;
using UnityEngine;

public class PlayerHealth : SingletonBehaviour<PlayerHealth>
{
    private PlayerAnimator _playerAnimator;

    private int _health = 100;
    private int _maxHealth = 100;

    private bool _isDeath = false;
    public bool IsDeath { get { return _isDeath; } }

    public static event Action OnHealthChange;
    public static event Action<int, int> OnHealthValueUpdate;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        _playerAnimator = GetComponent<PlayerAnimator>();
        base.Init();
    }

    private void Start()
    {
        OnHealthValueUpdate?.Invoke(Health, _maxHealth);
    }

    public int Health
    {
        get => _health;
        private set
        {
            _health = value;
            OnHealthValueUpdate?.Invoke(_health, _maxHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        if (amount < 0) return;
        if(Health <= 0) return;
        Health = Mathf.Max(Health - amount, 0);

        CameraController.Instance.StartShake(0.7f);

        if (Health <= 0)
        {
            UIManager.Instance.OpenUI<GameOverUI>(new BaseUIData());
            _isDeath = true;
            _playerAnimator.PlayDeathAnimation();
        }

        else
        {
            OnHealthChange?.Invoke();
            _playerAnimator.PlayHitAnimation();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0) return;
        Health = Mathf.Min(Health + amount, _maxHealth);
    }

    public void Die()
    {
        TakeDamage(Health);
    }
}
