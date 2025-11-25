using UnityEngine;

public class Mage : CharacterBase
{
    public override ECharacterType CharacterType => ECharacterType.Mage;
    [SerializeField] private GameObject _magicBoltPrefab;

    public override void Attack(Vector2 position, float power, int direction)
    {
        // NOTE : 이후 Pooling 방식을 사용해 오브젝트 관리
        GameObject magicBoltObject = Instantiate(_magicBoltPrefab, position, Quaternion.identity);
        magicBoltObject.GetComponent<ProjectileBase>().SetProjectileInfo(direction, power * _attackDamage);
    }
}
