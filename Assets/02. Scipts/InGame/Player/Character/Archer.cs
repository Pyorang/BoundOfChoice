using UnityEngine;

public class Archer : CharacterBase
{
    public override ECharacterType CharacterType => ECharacterType.Archer;

    public override void Attack(Vector2 position, float power, int direction)
    {
        GameObject arrowObject = PoolManager.Instance.GetObject(EPoolType.Arrow);
        arrowObject.GetComponent<ProjectileBase>().Init(position, direction, power * _attackDamage);
    }
}
