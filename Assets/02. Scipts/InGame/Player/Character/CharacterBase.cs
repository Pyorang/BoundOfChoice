using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    public abstract ECharacterType CharacterType { get; }
    [SerializeField] protected float _attackCoolTime = 1.0f;
    private float _lastAttackTime;

    public void DeactivateCharacter()
    {
        this.gameObject.SetActive(false);
    }

    public void ActivateCharacter()
    {
        this.gameObject.SetActive(true);
    }

    public abstract void Attack(Vector2 position, float power, int direction);


#if UNITY_EDITOR
    public virtual void DrawRange(Vector2 position, int direction) { }
#endif

    public bool CanAttack() => Time.time - _lastAttackTime >= _attackCoolTime;
    public void ResetAttackCooldown() => _lastAttackTime = Time.time;
}
