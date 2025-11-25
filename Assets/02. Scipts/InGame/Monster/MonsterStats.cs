using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    [Header("몬스터 데이터")]
    [SerializeField] private MonsterData _baseData;

    private float _currentHealth;
    private float _currentMaxHealth;
    private float _currentAttackPower;
    private float _currentMoveSpeed;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _currentMaxHealth;
    public float AttackPower => _currentAttackPower;
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

    public void TakeDamage(float damage)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }
}