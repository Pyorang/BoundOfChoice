using System;
using UnityEngine;

public class PlayerMovement : SingletonBehaviour<PlayerMovement>
{
    private PlayerAnimator _playerAnimator;

    public static event Action<int> OnSpeedChanged;

    private bool _isOnGround = false;

    [Header("이동 설정")]
    [Space]
    [SerializeField] private int _minMoveSpeed = 3;
    [SerializeField] private int _maxMoveSpeed = 13;
    [SerializeField] private int _moveSpeed;
    [SerializeField] private float _jumpForce = 5;
    private float _xMovement = 0.0f;

    private int _additionalMoveSpeed = 0;
    public int AdditionalMoveSpeed
    {
        get => _additionalMoveSpeed;
        set
        {
            if (value < 0)
            {
                return;
            }

            _additionalMoveSpeed = value;
            OnSpeedChanged?.Invoke(value);
            _moveSpeed = Mathf.Clamp(_minMoveSpeed + _additionalMoveSpeed, _minMoveSpeed, _maxMoveSpeed);
        }
    }

    public bool Moving => _xMovement != 0.0f || !_isOnGround;

    private int _playerDirection = 1;
    public int PlayerDirection => _playerDirection;

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
        _moveSpeed = _minMoveSpeed;
        OnSpeedChanged?.Invoke(AdditionalMoveSpeed);
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

        _rigidBody.linearVelocityX = _xMovement * _moveSpeed;
    }

    public void ResetSpeed()
    {
        AdditionalMoveSpeed = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground")) return;
        _playerAnimator.StopJumpAnimation();
        _isOnGround = true;
    }
}
