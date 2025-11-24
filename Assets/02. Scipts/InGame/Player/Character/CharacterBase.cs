using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField] protected float _attackDamage = 10.0f;

    public abstract void Attack(Vector2 position, float power, int direction);
    public virtual void DrawRange(Vector2 position, int direction) { }
}
