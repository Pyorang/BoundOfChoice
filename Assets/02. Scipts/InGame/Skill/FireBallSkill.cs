using UnityEngine;

public class FireBallSkill : SkillBase
{
    public override ESkillType SkillType => ESkillType.FireBall;

    protected override void ExecuteSkill(int direction, int additionalDamage = 0)
    {
        GameObject fireBallObject = PoolManager.Instance.GetObject(EPoolType.FireBall);
        ProjectileBase projectile = fireBallObject.GetComponent<ProjectileBase>();
        if (projectile == null) return;
        projectile.Init(PlayerHealth.Instance.gameObject.transform.position, direction, additionalDamage);
    }
}
