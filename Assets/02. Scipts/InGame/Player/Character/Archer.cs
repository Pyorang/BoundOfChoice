using UnityEngine;

public class Archer : CharacterBase
{
    public override ECharacterType CharacterType => ECharacterType.Archer;
    private GameObject _arrowPrefab;

    private void Awake()
    {
        _arrowPrefab = Resources.Load<GameObject>("InGame/Arrow");
    }

    public override void Attack(Vector2 position, float power, int direction)
    {
        // NOTE : 이후 Pooling 방식을 사용해 오브젝트 관리
        GameObject arrowObject = Instantiate(_arrowPrefab, position, Quaternion.identity);
        arrowObject.GetComponent<Arrow>().SetArrowInfo(direction, power * _attackDamage);
    }
}
