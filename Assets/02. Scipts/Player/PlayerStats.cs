using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private PlayerBaseStats _baseStats;
    private int _health;
    private int _mana;
    private float _moveSpeed;
    private float _attackPower;
    private float _jumpForce;

    public int Health => _health;
    public int Mana => _mana;
    public float MoveSpeed => _moveSpeed;
    public float AttackPower => _attackPower;
    public float JumpForce => _jumpForce;

    private void Awake()
    {
        _baseStats = Resources.Load<PlayerBaseStats>("DataTable/PlayerBaseStats");
        _moveSpeed = _baseStats.MinSpeed;
        _jumpForce = _baseStats.JumpHeight;
        _attackPower = _baseStats.AttackPower;
        _health = _baseStats.MaxHealth;
        _mana = _baseStats.MaxMana;
    }

    public void SpeedUp(float amount)
    {
        if (amount < 0) return;
        _moveSpeed = Mathf.Min(_moveSpeed + amount, _baseStats.MaxSpeed);
    }

    public void SpeedDown(float amount)
    {
        if (amount < 0) return;
        _moveSpeed = Mathf.Max(_moveSpeed - amount, _baseStats.MinSpeed);
    }

    public void PowerUp(float amount)
    {
        if (amount < 0) return;
        _attackPower += amount;
    }

    public void PowerDown(float amount)
    {
        if (amount < 0) return;
        _attackPower = Mathf.Max(_attackPower - amount, 0);
    }

    public void TakeDamage(int amount)
    {
        if (amount < 0) return;
        _health = Mathf.Max(_health - amount, 0);
    }

    public void Heal(int amount)
    {
        if (amount < 0) return;
        _health = Mathf.Min(_health + amount, _baseStats.MaxHealth);
    }

    public void UseMana(int amount)
    {
        if (amount < 0) return;
        _mana = Mathf.Max(_mana - amount, 0);
    }

    public void RegenerateMana(int amount)
    {
        if (amount < 0) return;
        _mana = Mathf.Min(_mana + amount, _baseStats.MaxMana);
    }
}
