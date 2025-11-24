using UnityEngine;

public class Archer : CharacterBase
{
    private GameObject _arrowObject;

    private void Awake()
    {
        _arrowObject = Resources.Load<GameObject>("InGame/Arrow");
    }

    public override void Attack(Vector2 position, float power, int direction)
    {
        GameObject arrow = Instantiate(_arrowObject, position, Quaternion.identity);
        arrow.GetComponent<Arrow>().SetArrowInfo(direction, power * _attackDamage);
    }
}
