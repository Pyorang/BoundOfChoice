using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats _playerStats;

    private Rigidbody2D _rigidBody;
    private float _xMovement = 0.0f;

    [Header("충돌 처리")]
    private LayerMask _groundLayer;
    private float _groundDistance = 0.1f;
    
    private LayerMask _wallLayer;
    private float _wallDistance = 0.1f;

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
        _rigidBody = GetComponent<Rigidbody2D>();

        _groundLayer = LayerMask.GetMask("Ground");
        _wallLayer = LayerMask.GetMask("Wall");

        Collider2D myCollider = GetComponent<Collider2D>();
        _groundDistance += myCollider.bounds.extents.y;
        _wallDistance += myCollider.bounds.extents.x;
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
            if (CheckOnGround() == false) return;
            Jump();
        }
    }

    private bool CheckOnGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, _groundDistance, _groundLayer);
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector2.up * _playerStats.JumpForce, ForceMode2D.Impulse);
    }

    private bool CheckReachedToWall()
    {
        return Physics2D.Raycast(transform.position, Vector2.right * _xMovement, _wallDistance, _wallLayer);
    }

    private void MoveHorizontal()
    {
        if (CheckReachedToWall() == true) return;
        _rigidBody.linearVelocityX = _xMovement * _playerStats.MoveSpeed;
    }
}
