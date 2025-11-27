using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController[] _animatorContoller;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private readonly int _attack = Animator.StringToHash("Attack");
    private readonly int _hit = Animator.StringToHash("Hit");
    private readonly int _death = Animator.StringToHash("Death");
    private readonly int _jump = Animator.StringToHash("Jump");
    private readonly int _isRun = Animator.StringToHash("IsRun");
    private readonly int _isOnGround = Animator.StringToHash("IsOnGround");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // NOTE : 애니메이션 테스트용입니다.
        float horizontal = Input.GetAxisRaw("Horizontal");
        int direction = (int)Mathf.Sign(horizontal);
        if (horizontal != 0) PlayRunAnimation(direction != 1);
        else StopRunAnimation();
        if (Input.GetKeyDown(KeyCode.Z)) PlayAttackAnimation();
        if (Input.GetKeyDown(KeyCode.X)) PlayHitAnimation();
        if (Input.GetKeyDown(KeyCode.C)) PlayDeathAnimation();
        if (Input.GetKeyDown(KeyCode.UpArrow)) PlayJumpAnimation();

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) ChangeAnimatorController(0);
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) ChangeAnimatorController(1);
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // NOTE : 애니메이션 테스트용입니다.
        if (!other.gameObject.CompareTag("Ground")) return;
        StopJumpAnimation();
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(_attack);
    }

    public void PlayHitAnimation()
    {
        _animator.SetTrigger(_hit);
    }

    public void PlayDeathAnimation()
    {
        _animator.SetTrigger(_death);
    }

    public void PlayJumpAnimation()
    {
        _animator.SetTrigger(_jump);
        _animator.SetBool(_isOnGround, false);
    }

    public void StopJumpAnimation()
    {
        _animator.SetBool(_isOnGround, true);
    }

    public void PlayRunAnimation(bool flip)
    {
        _animator.SetBool(_isRun, true);
        _spriteRenderer.flipX = flip;
    }

    public void StopRunAnimation()
    {
        _animator.SetBool(_isRun, false);
    }

    public void ChangeAnimatorController(int index)
    {
        _animator.runtimeAnimatorController = _animatorContoller[index];
    }
}
