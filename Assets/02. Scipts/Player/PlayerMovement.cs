using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats _playerStats;

    private Rigidbody2D _rigidBody;
    private float _xMovement = 0.0f;

    private bool _isOnGround = false;

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
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
        _rigidBody.AddForce(Vector2.up * _playerStats.JumpForce, ForceMode2D.Impulse);
    }

    private void MoveHorizontal()
    {
        _rigidBody.linearVelocityX = _xMovement * _playerStats.MoveSpeed;
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
