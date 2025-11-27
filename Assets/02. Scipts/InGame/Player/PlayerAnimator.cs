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
