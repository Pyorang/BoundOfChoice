using UnityEngine;

public class Archer : CharacterBase
{
    public override ECharacterType CharacterType => ECharacterType.Archer;

    public override void Attack(Vector2 position, float power, int direction)
    {
        if (!InventoryUI.Instance.TryConsumeItem(EItemType.ArrowItem, 1)) return;

        GameObject arrowObject = PoolManager.Instance.GetObject(EPoolType.Arrow);
        arrowObject.GetComponent<ProjectileBase>().SetProjectileInfo(position, direction, power * _attackDamage);
    }
}
