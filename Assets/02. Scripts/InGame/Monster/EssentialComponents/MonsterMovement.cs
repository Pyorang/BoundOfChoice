using UnityEngine;

[RequireComponent(typeof(MonsterStats), typeof(Rigidbody2D))]
public class MonsterMovement : MonoBehaviour
{
    private MonsterStats _stats;
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;

    [Header("넉백 수치")]
    [Space]
    [SerializeField] private float _knockBackDuration;
    [SerializeField] private float _knockBackForce;
    private float _knockBackTimer;
    private float _knockBackVelocity;

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
        if (_knockBackTimer > 0)
        {
            _knockBackTimer -= Time.fixedDeltaTime;
            _rigidbody.linearVelocityX = _knockBackVelocity;
        }
        else
        {
            _rigidbody.linearVelocityX = _direction.x * _stats.MoveSpeed;
        }
    }

    public void SetMoveDirection(Vector2 direction)
    {
        _direction = direction.normalized;
    }

    public void StopMove()
    {
        _direction = Vector2.zero;
    }

    public void ApplyKnockback(float distance)
    {
        float directionX = -Mathf.Sign(distance);
        _knockBackVelocity = directionX * _knockBackForce;
        _knockBackTimer = _knockBackDuration;
    }
}