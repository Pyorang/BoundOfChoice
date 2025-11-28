using System;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    [Header("몬스터 데이터")]
    [SerializeField] private MonsterData _baseData;

    public event Action OnDeath;
    public event Action OnDamageTaken;

    private int _currentHealth;
    private int _currentMaxHealth;
    private int _currentAttackPower;
    private float _currentMoveSpeed;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _currentMaxHealth;
    public int AttackPower => _currentAttackPower;
    public float MoveSpeed => _currentMoveSpeed;

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        if (_baseData == null)
        {
            Debug.LogError("MonsterData is missing!");
            return;
        }

        _currentMaxHealth = _baseData.MaxHealth;
        _currentHealth = _currentMaxHealth;
        _currentAttackPower = _baseData.AttackPower;
        _currentMoveSpeed = _baseData.MoveSpeed;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            OnDeath?.Invoke();
            return;
        }

        OnDamageTaken?.Invoke();
    }

    public void SetMoveSpeed(float speed)
    {
        _currentMoveSpeed = speed;
    }

    public void ResetSpeed()
    {
        _currentMoveSpeed = _baseData.MoveSpeed;
    }
}