using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int _health = 100;
    private int _maxHealth = 100;

    private int _mana = 100;
    private int _maxMana = 100;

    private float _moveSpeed = 1;
    private float _minMoveSpeed = 1;
    private float _maxMoveSpeed = 10;

    private float _attackPower = 1;

    private float _jumpForce = 1;

    public int Health => _health;
    public int Mana => _mana;
    public float MoveSpeed => _moveSpeed;
    public float AttackPower => _attackPower;
    public float JumpForce => _jumpForce;

    public void MoveSpeedUp(float amount)
    {
        if (amount < 0) return;
        _moveSpeed = Mathf.Min(_moveSpeed + amount, _maxMoveSpeed);
    }

    public void MoveSpeedDown(float amount)
    {
        if (amount < 0) return;
        _moveSpeed = Mathf.Max(_moveSpeed - amount, _minMoveSpeed);
    }

    public void AttakPowerUp(float amount)
    {
        if (amount < 0) return;
        _attackPower += amount;
    }

    public void AttackPowerDown(float amount)
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
        _health = Mathf.Min(_health + amount, _maxHealth);
    }

    public void UseMana(int amount)
    {
        if (amount < 0) return;
        _mana = Mathf.Max(_mana - amount, 0);
    }

    public void RegenerateMana(int amount)
    {
        if (amount < 0) return;
        _mana = Mathf.Min(_mana + amount, _maxMana);
    }
}
