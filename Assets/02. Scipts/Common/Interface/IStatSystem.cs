using System;

public interface IStatSystem
{
    float CurrentHealth { get; }
    float MaxHealth { get; }
    float AttackPower { get; }
    float MoveSpeed { get; }
    bool IsDead { get; }

    void TakeDamage(float amount);
}
