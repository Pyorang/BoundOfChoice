using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController[] _animatorController;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private static readonly int s_attack = Animator.StringToHash("Attack");
    private static readonly int s_hit = Animator.StringToHash("Hit");
    private static readonly int s_death = Animator.StringToHash("Death");
    private static readonly int s_jump = Animator.StringToHash("Jump");
    private static readonly int s_isRun = Animator.StringToHash("IsRun");
    private static readonly int s_isOnGround = Animator.StringToHash("IsOnGround");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

#if UNITY_EDITOR
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
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) ChangeAnimatorController(2);
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // NOTE : 애니메이션 테스트용입니다.
        if (!other.gameObject.CompareTag("Ground")) return;
        StopJumpAnimation();
    }
#endif

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(s_attack);
    }

    public void PlayHitAnimation()
    {
        _animator.SetTrigger(s_hit);
    }

    public void PlayDeathAnimation()
    {
        _animator.SetTrigger(s_death);
    }

    public void PlayJumpAnimation()
    {
        if (!_animator.GetBool(s_isOnGround)) return;
        _animator.SetTrigger(s_jump);
        _animator.SetBool(s_isOnGround, false);
    }

    public void StopJumpAnimation()
    {
        _animator.SetBool(s_isOnGround, true);
    }

    public void PlayRunAnimation(bool flip)
    {
        _animator.SetBool(s_isRun, true);
        _spriteRenderer.flipX = flip;
    }

    public void StopRunAnimation()
    {
        _animator.SetBool(s_isRun, false);
    }

    public void ChangeAnimatorController(int index)
    {
        _animator.runtimeAnimatorController = _animatorController[index];
    }
}
