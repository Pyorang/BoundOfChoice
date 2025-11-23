using UnityEngine;

public class ChaseMonsterController : MonsterController
{
    protected override Vector2 GetMoveDirection()
    {
        float sign = Mathf.Sign(_distance);
        return Vector2.right * sign;
    }
}