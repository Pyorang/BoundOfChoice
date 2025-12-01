using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    private Animator _animator;

    private static readonly int s_isMoving = Animator.StringToHash("IsMoving");
    private static readonly int s_attack = Animator.StringToHash("Attack");
    private static readonly int s_specialAttack = Animator.StringToHash("SpecialAttack");
    private static readonly int s_hit = Animator.StringToHash("Hit");
    private static readonly int s_death = Animator.StringToHash("Death");

    private const float FlipRotation = 180f;
    private const float NormalRotation = 0f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayMoveAnimation(bool isMoving)
    {
        _animator.SetBool(s_isMoving, isMoving);
    }

    public void PlayAttackAnimation()
    {
        PlayMoveAnimation(false);
        _animator.SetTrigger(s_attack);
    }

    public void PlaySpecialAttackAnimation()
    {
        _animator.SetTrigger(s_specialAttack);
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
        float rotation = flip ? FlipRotation : NormalRotation;
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}