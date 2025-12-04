using System;
using UnityEngine;

public class PlayerHealth : SingletonBehaviour<PlayerHealth>
{
    private PlayerAnimator _playerAnimator;

    private int _health = 100;
    public int Health
    {
        get => _health;
        private set
        {
            if (_health == value) return;

            bool isHealed = _health < value ? true : false;
            _health = value;
            OnHealthChange?.Invoke(isHealed);
            OnHealthValueUpdate?.Invoke(_health, MaxHealth);
        }
    }

    private int _maxHealth = 100;
    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
            OnHealthValueUpdate?.Invoke(Health, _maxHealth);
        }
    }

    private bool _isDeath = false;
    public bool IsDeath { get { return _isDeath; } }

    private static readonly int _maxBloodNumber = 3;

    public static event Action<bool> OnHealthChange;
    public static event Action<int, int> OnHealthValueUpdate;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        _playerAnimator = GetComponent<PlayerAnimator>();
        base.Init();
    }

    private void Start()
    {
        OnHealthValueUpdate?.Invoke(Health, MaxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (amount < 0) return;
        if(Health <= 0) return;
     
        Health = Mathf.Max(Health - amount, 0);

        SpawnRandomBlood();
        CameraController.Instance.StartShake(0.7f);

        if (Health <= 0)
        {
            UIManager.Instance.OpenUI<GameOverUI>(new BaseUIData());
            _isDeath = true;
            AudioManager.Instance.Play(AudioType.SFX, "PlayerDie");
            _playerAnimator.PlayDeathAnimation();
        }

        else
        {
            _playerAnimator.PlayHitAnimation();
        }
    }

    private void SpawnRandomBlood()
    {
        int randomNumber = UnityEngine.Random.Range(1, _maxBloodNumber + 1);
        string PoolingObjectName = $"Blood{randomNumber}";
        EPoolType poolingObject = (EPoolType)Enum.Parse(typeof(EPoolType), PoolingObjectName, true);

        GameObject blood = PoolManager.Instance.GetObject(poolingObject, true);
        blood.transform.position = transform.position;
    }

    public void Heal(int amount)
    {
        if (amount < 0) return;
        Health = Mathf.Min(Health + amount, MaxHealth);
    }

    public void Die()
    {
        TakeDamage(Health);
    }
}
