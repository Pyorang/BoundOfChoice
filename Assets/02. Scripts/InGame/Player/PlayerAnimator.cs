using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController[] _animatorController;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int IsRun = Animator.StringToHash("IsRun");
    private static readonly int IsOnGround = Animator.StringToHash("IsOnGround");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(Attack);
    }

    public void PlayHitAnimation()
    {
        _animator.SetTrigger(Hit);
    }

    public void PlayDeathAnimation()
    {
        _animator.SetTrigger(Death);
    }

    public void PlayJumpAnimation()
    {
        if (!_animator.GetBool(IsOnGround)) return;
        _animator.SetTrigger(Jump);
        _animator.SetBool(IsOnGround, false);
    }

    public void StopJumpAnimation()
    {
        _animator.SetBool(IsOnGround, true);
    }

    public void PlayRunAnimation(bool flip)
    {
        _animator.SetBool(IsRun, true);
        _spriteRenderer.flipX = flip;
    }

    public void StopRunAnimation()
    {
        _animator.SetBool(IsRun, false);
    }

    public void ChangeAnimatorController(int index)
    {
        _animator.runtimeAnimatorController = _animatorController[index];
    }
}
