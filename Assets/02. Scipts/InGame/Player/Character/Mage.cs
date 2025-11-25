using UnityEngine;

public class Mage : CharacterBase
{
    public override ECharacterType CharacterType => ECharacterType.Mage;

    public override void Attack(Vector2 position, float power, int direction)
    {
        GameObject magicBoltObject = PoolManager.Instance.GetObject(EPoolType.MagicBolt);
        magicBoltObject.GetComponent<ProjectileBase>().SetProjectileInfo(position, direction, power * _attackDamage);
    }
}
