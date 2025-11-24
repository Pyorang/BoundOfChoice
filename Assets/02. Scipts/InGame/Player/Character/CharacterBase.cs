using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField] protected float _attackDamage = 10.0f;
    [SerializeField] protected float _attackCoolTime = 1.0f;
    private float _lastAttackTime;

    public abstract void Attack(Vector2 position, float power, int direction);
    public virtual void DrawRange(Vector2 position, int direction) { }

    public bool CanAttack()
    {
        float currentTime = Time.time;
        float duration = currentTime - _lastAttackTime;
        if (duration < _attackCoolTime) return false;
        _lastAttackTime = currentTime;
        return true;
    }
}
