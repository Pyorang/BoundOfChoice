using System.Collections.Generic;
using UnityEngine;

public class Mage : CharacterBase
{
    public override ECharacterType CharacterType => ECharacterType.Mage;
    
    private ESkillType _defaultSkill = ESkillType.MagicBolt;
    private Dictionary<ESkillType, SkillBase> _skills = new Dictionary<ESkillType, SkillBase>();

    protected override void Init()
    {
        SkillBase[] skills = GetComponents<SkillBase>();
        foreach (SkillBase skill in skills)
        {
            _skills.Add(skill.SkillType, skill);
        }
    }

    public override void Attack(int direction, int additionalDamage)
    {
        _skills[_defaultSkill].UseSkill(direction, additionalDamage);
    }

    public override void UseSkill(ESkillType type, int direction, int additionalDamage)
    {
        if (_skills.TryGetValue(type, out SkillBase skill))
        {
            skill.UseSkill(direction, additionalDamage);
        }
    }

#if UNITY_EDITOR
    public override void DrawRange(Vector2 position, int direction)
    {
        IceAgeSkill iceAgeSkill = _skills[ESkillType.IceAge] as IceAgeSkill;
        iceAgeSkill?.DrawRange(position, direction);
    }
#endif
}
