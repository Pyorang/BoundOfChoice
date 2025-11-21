using UnityEngine;

[RequireComponent(typeof(MonsterStats), typeof(Rigidbody2D))]
public class MonsterMovement : MonoBehaviour
{
    private MonsterStats _stats;
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;


    private void Awake()
    {
        _stats = GetComponent<MonsterStats>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.linearVelocity = _direction * _stats.MoveSpeed;
    }

    public void SetMoveDirection(Vector2 direction)
    {
        _direction = direction.normalized;
    }
}
