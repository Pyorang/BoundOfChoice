using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    private Animator _animator;

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int SpecialAttack = Animator.StringToHash("SpecialAttack");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Death = Animator.StringToHash("Death");

    private const float FlipRotation = 180f;
    private const float NormalRotation = 0f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayMoveAnimation(bool isMoving)
    {
        _animator.SetBool(IsMoving, isMoving);
    }

    public void PlayAttackAnimation()
    {
        PlayMoveAnimation(false);
        _animator.SetTrigger(Attack);
    }

    public void PlaySpecialAttackAnimation()
    {
        _animator.SetTrigger(SpecialAttack);
    }

    public void PlayHitAnimation()
    {
        _animator.SetTrigger(Hit);
    }

    public void PlayDeathAnimation()
    {
        _animator.SetTrigger(Death);
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