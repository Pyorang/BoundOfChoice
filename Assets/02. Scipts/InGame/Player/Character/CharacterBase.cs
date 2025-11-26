using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    public abstract ECharacterType CharacterType { get; }
    [SerializeField] protected float _attackCoolTime = 1.0f;
    private float _lastAttackTime;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init() { }

    public void DeactivateCharacter()
    {
        this.gameObject.SetActive(false);
    }

    public void ActivateCharacter()
    {
        this.gameObject.SetActive(true);
    }

    public abstract void Attack(int direction, int additionalDamage);

    public virtual void UseSkill(ESkillType type, int direction, int additionalDamage) { }

#if UNITY_EDITOR
    public virtual void DrawRange(Vector2 position, int direction) { }
#endif

    public bool CanAttack() => Time.time - _lastAttackTime >= _attackCoolTime;
    public void ResetAttackCooldown() => _lastAttackTime = Time.time;
}
