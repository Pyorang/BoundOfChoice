using UnityEngine;

public class PatrollMonsterController : MonsterController
{
    [Header("이동 범위")]
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    private float _nextPosition;

    protected override void Init()
    {
        _nextPosition = transform.position.x;
    }

    protected override Vector2 GetMoveDirection()
    {
        float distance = _nextPosition - transform.position.x;
        float absDistance = Mathf.Abs(distance);

        if (absDistance < Mathf.Epsilon)
        {
            _nextPosition = Random.Range(_minX, _maxX);
        }

        float sign = Mathf.Sign(distance);
        return Vector2.right * sign;
    }
}
