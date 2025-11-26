using UnityEngine;

public class Archer : CharacterBase
{
    public override ECharacterType CharacterType => ECharacterType.Archer;

    public override void Attack(int direction, int additionalDamage)
    {
        GameObject arrowObject = PoolManager.Instance.GetObject(EPoolType.Arrow);
        arrowObject.GetComponent<ProjectileBase>().Init(this.transform.position, direction, additionalDamage);
    }
}
