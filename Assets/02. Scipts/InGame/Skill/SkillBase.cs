using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public abstract ESkillType SkillType { get; }
    [Header("공격")]
    [Space]
    [SerializeField] private int _cost;
    [SerializeField] protected int _damage;

    public void UseSkill(int direction, int additionalDamage = 0)
    {
        if (!PlayerMana.Instance.TryUseMana(_cost)) return;
        ExecuteSkill(direction, additionalDamage);
    }

    protected abstract void ExecuteSkill(int direction, int additionalDamage = 0);
}
