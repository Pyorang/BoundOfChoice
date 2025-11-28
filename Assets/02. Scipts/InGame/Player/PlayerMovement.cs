using System;
using UnityEngine;

public class PlayerMovement : SingletonBehaviour<PlayerMovement>
{
    private PlayerAnimator _playerAnimator;

    public static event Action<int> OnSpeedChanged;

    private bool _isOnGround = false;

    [Header("이동 설정")]
    [Space]
    [SerializeField] private float _moveSpeed = 1.0f;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private float _maxMoveSpeed = 10.0f;
    private readonly float _minMoveSpeed = 1.0f;
    private float _xMovement = 0.0f;

    public bool Moving => _xMovement != 0.0f || !_isOnGround;

    private int _playerDirection = 1;
    public int PlayerDirection => _playerDirection;

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

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        Init();
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
        if(PlayerHealth.Instance.IsDeath)
        {
            _xMovement = 0.0f;
            return;
        }

        _xMovement = Input.GetAxisRaw("Horizontal");

        if (_xMovement != 0)
        {
            _playerDirection = (int)_xMovement;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!_isOnGround) return;
        _isOnGround = false;
        _playerAnimator.PlayJumpAnimation();
        _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void MoveHorizontal()
    {
        if(_xMovement != 0)
        {
            _playerAnimator.PlayRunAnimation(_xMovement != 1);
        }
        else
        {
            _playerAnimator.StopRunAnimation();
        }

        _rigidBody.linearVelocityX = _xMovement * MoveSpeed;
    }

    public void IncreaseSpeed(float amount)
    {
        if (amount < 0.0f) return;
        MoveSpeed = Mathf.Min(MoveSpeed + amount, _maxMoveSpeed);
    }

    public void DecreaseSpeed(float amount)
    {
        if (amount < 0.0f) return;
        MoveSpeed = Mathf.Max(MoveSpeed - amount, _minMoveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground")) return;
        _playerAnimator.StopJumpAnimation();
        _isOnGround = true;
    }
}
