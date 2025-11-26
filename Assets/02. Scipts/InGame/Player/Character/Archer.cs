using UnityEngine;

public class Archer : CharacterBase
{
    public override ECharacterType CharacterType => ECharacterType.Archer;

    public override void Attack(int direction, int additionalDamage)
    {
        if (!InventoryUI.Instance.TryConsumeItem(EItemType.ArrowItem)) return;

        GameObject arrowObject = PoolManager.Instance.GetObject(EPoolType.Arrow);
        arrowObject.GetComponent<ProjectileBase>().Init(this.transform.position, direction, additionalDamage);
    }
}
