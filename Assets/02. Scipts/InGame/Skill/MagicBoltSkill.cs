using UnityEngine;

public class MagicBoltSkill : SkillBase
{
    public override ESkillType SkillType => ESkillType.MagicBolt;

    protected override void ExecuteSkill(int direction, int additionalDamage = 0)
    {
        GameObject magicBoltObject = PoolManager.Instance.GetObject(EPoolType.MagicBolt);
        ProjectileBase projectile = magicBoltObject.GetComponent<ProjectileBase>();
        if (projectile == null) return;
        projectile.Init(this.transform.position, direction, additionalDamage);
    }
}
