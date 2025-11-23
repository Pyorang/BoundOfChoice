using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    [Header("이동 설정")]
    private float _xMovement = 0.0f;
    private float _moveSpeed = 1.0f;
    private readonly float _minMoveSpeed = 1.0f;
    private readonly float _maxMoveSpeed = 10.0f;

    [Header("점프 설정")]
    private float _jumpForce = 5;
    private bool _isOnGround = false;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetKeyInput();
    }

    private void FixedUpdate()
    {
        MoveHorizontal();
    }

    private void GetKeyInput()
    {
        _xMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!_isOnGround) return;
        _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void MoveHorizontal()
    {
        _rigidBody.linearVelocityX = _xMovement * _moveSpeed;
    }

    public void MoveSpeedUp(float amount)
    {
        if (amount < 0.0f) return;
        _moveSpeed = Mathf.Min(_moveSpeed + amount, _maxMoveSpeed);
    }

    public void MoveSpeedDown(float amount)
    {
        if (amount < 0.0f) return;
        _moveSpeed = Mathf.Max(_moveSpeed - amount, _minMoveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground")) return;
        _isOnGround = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground")) return;
        _isOnGround = false;
    }
}
