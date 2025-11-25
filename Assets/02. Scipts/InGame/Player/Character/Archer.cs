using UnityEngine;

public class Archer : CharacterBase
{
    public override ECharacterType CharacterType => ECharacterType.Archer;
    [SerializeField] private GameObject _arrowPrefab;

    public override void Attack(Vector2 position, float power, int direction)
    {
        // NOTE : 이후 Pooling 방식을 사용해 오브젝트 관리
        GameObject arrowObject = Instantiate(_arrowPrefab, position, Quaternion.identity);
        arrowObject.GetComponent<ProjectileBase>().SetProjectileInfo(direction, power * _attackDamage);
    }
}
