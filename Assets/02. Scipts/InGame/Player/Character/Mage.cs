using UnityEngine;

public class Mage : CharacterBase
{
    public override ECharacterType CharacterType => ECharacterType.Mage;
    private GameObject _magicBoltPrefab;
    [SerializeField] int _maxBoltCount = 4;

    // Note : Pooling 방식 적용시 Bolt Counting 방식 수정
    public static int BoltCount = 0;

    private void Awake()
    {
        _magicBoltPrefab = Resources.Load<GameObject>("InGame/MagicBolt");
    }

    public override void Attack(Vector2 position, float power, int direction)
    {
        if (BoltCount >= _maxBoltCount) return;
        BoltCount += 1;

        // NOTE : 이후 Pooling 방식을 사용해 오브젝트 관리
        GameObject magicBoltObject = Instantiate(_magicBoltPrefab, position, Quaternion.identity);
        magicBoltObject.GetComponent<ProjectileBase>().SetProjectileInfo(direction, power * _attackDamage);
    }
}
