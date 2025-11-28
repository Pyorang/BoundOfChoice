using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private static readonly int s_isMoving = Animator.StringToHash("IsMoving");
    private static readonly int s_attack = Animator.StringToHash("Attack");
    private static readonly int s_hit = Animator.StringToHash("Hit");
    private static readonly int s_death = Animator.StringToHash("Death");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayMoveAnimation(bool isMoving)
    {
        _animator.SetBool(s_isMoving, isMoving);
    }

    public void StopMoveAnimation()
    {
        _animator.SetBool(s_isMoving, false);
    }

    public void PlayAttackAnimation()
    {
        StopMoveAnimation();
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

    public void StopAnimation()
    {
        _animator.speed = 0.0f;
    }

    public void ResumeAnimation()
    {
        _animator.speed = 1.0f;
    }
    public void SetSpriteFlip(bool flip)
    {
        _spriteRenderer.flipX = flip;
    }
}