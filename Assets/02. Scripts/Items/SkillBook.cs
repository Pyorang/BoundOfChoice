using UnityEngine;

public class SkillBook : ItemBase
{
    [SerializeField] private SkillBase _skill;

    public override bool ApplyEffect()
    {
        _skill.UseSkill();
        return false;
    }
}
