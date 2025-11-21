using Unity.VisualScripting;
using UnityEngine;

public class ChaseMonsterController : MonsterController
{
    protected override Vector2 GetMoveDirection()
    {
        float distance = _player.transform.position.x - transform.position.x;

        float sign = Mathf.Sign(distance);
        return Vector2.right * sign;
    }
}
