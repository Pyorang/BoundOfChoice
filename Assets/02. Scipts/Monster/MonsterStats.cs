using UnityEngine;

public class MonsterStats : MonoBehaviour, IStatSystem
{
    [Header("데이터 소스")]
    [SerializeField] private MonsterData _baseData;

    private float _currentHealth;
    private float _currentMaxHealth;
    private float _currentAttackPower;
    private float _currentMoveSpeed;

    private bool _isDead;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _currentMaxHealth;
    public float AttackPower => _currentAttackPower;
    public float MoveSpeed => _currentMoveSpeed;
    public bool IsDead => _isDead;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        if(_baseData == null)
        {
            Debug.LogError("MonsterData is missing!");
            return;
        }

        _currentMaxHealth = _baseData.MaxHealth;
        _currentHealth = _currentMaxHealth;
        _currentAttackPower = _baseData.AttackPower;
        _currentMoveSpeed = _baseData.MoveSpeed;
        _isDead = false;
    }

    public void TakeDamage(float damage)
    {
        if (_isDead) return;

        _currentHealth -= damage;

        if (_currentHealth > 0) return;
        Death();
    }

    private void Death()
    {
        _isDead = true;
        gameObject.SetActive(false);
    }
}
