using System.Collections;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    [Header("몬스터 데이터")]
    [SerializeField] private MonsterData _baseData;

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
        _currentHealth -= damage;
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