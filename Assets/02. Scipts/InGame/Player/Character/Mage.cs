using UnityEngine;

public class Mage : CharacterBase
{
    public override ECharacterType CharacterType => ECharacterType.Mage;

    public override void Attack(Vector2 position, float power, int direction)
    {
        GameObject magicBoltObject = PoolManager.Instance.GetObject(EPoolType.MagicBolt);
        ProjectileBase projectile = magicBoltObject.GetComponent<ProjectileBase>();
        if (projectile == null) return;
        if (projectile.TryConsumeCost() == false) return;
        projectile.Init(position, direction, power * _attackDamage);
    }
}
