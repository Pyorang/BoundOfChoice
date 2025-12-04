using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public abstract ESkillType SkillType { get; }
    [Header("스폰 위치")]
    [SerializeField] protected Vector2 _spawnOffset;

    [Header("공격")]
    [Space]
    [SerializeField] private int _cost;

    public void UseSkill()
    {
        if(PlayerCombat.Instance.CurrentCharacter == ECharacterType.Mage)
        {
            if (!PlayerMana.Instance.TryUseMana(_cost)) return;
            ExecuteSkill(PlayerMovement.Instance.PlayerDirection, PlayerCombat.Instance.AdditionalPower);
        }   
    }

    protected abstract void ExecuteSkill(int direction, int additionalDamage = 0);
}
