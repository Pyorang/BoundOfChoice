using UnityEngine;

public class FireBallSkill : SkillBase
{
    public override ESkillType SkillType => ESkillType.FireBall;

    protected override void ExecuteSkill(int direction, int additionalDamage = 0)
    {
        AudioManager.Instance.Play(AudioType.SFX, "FireBall");

        GameObject fireBallObject = PoolManager.Instance.GetObject(EPoolType.FireBall);
        ProjectileBase projectile = fireBallObject.GetComponent<ProjectileBase>();
        if (projectile == null) return;
        Vector2 directionalSpawnOffset = new Vector2(_spawnOffset.x * direction, _spawnOffset.y);
        Vector2 spawnPosition = (Vector2)PlayerHealth.Instance.transform.position + directionalSpawnOffset; 
        projectile.Init(spawnPosition, direction, additionalDamage);
    }
}
