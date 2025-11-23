using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static event Action<int> OnSpeedChanged;

    private bool _isOnGround = false;

    [Header("이동 설정")]
    [Space]
    [SerializeField] private float _moveSpeed = 1.0f;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private float _maxMoveSpeed = 10.0f;
    private readonly float _minMoveSpeed = 1.0f;
    private float _xMovement = 0.0f;

    public float MoveSpeed
    {
        get => _moveSpeed;
        private set
        {
            _moveSpeed = Mathf.Clamp(value, _minMoveSpeed, _maxMoveSpeed);
            OnSpeedChanged?.Invoke((int)_moveSpeed);
        }
    }

    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        OnSpeedChanged?.Invoke((int)MoveSpeed);
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
        _rigidBody.linearVelocityX = _xMovement * MoveSpeed;
    }

    public void MoveSpeedUp(float amount)
    {
        if (amount < 0.0f) return;
        MoveSpeed = Mathf.Min(MoveSpeed + amount, _maxMoveSpeed);
    }

    public void MoveSpeedDown(float amount)
    {
        if (amount < 0.0f) return;
        MoveSpeed = Mathf.Max(MoveSpeed - amount, _minMoveSpeed);
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
