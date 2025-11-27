using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly int _isMoving = Animator.StringToHash("IsMoving");
    private readonly int _attack = Animator.StringToHash("Attack");
    private readonly int _hurt = Animator.StringToHash("Hurt");
    private readonly int _die = Animator.StringToHash("Die");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayMoveAnimation(bool isMoving)
    {
        _animator.SetBool(_isMoving, isMoving);
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(_attack);
    }

    public void PlayHurtAnimation()
    {
        _animator.SetTrigger(_hurt);
    }

    public void PlayDieAnimation()
    {
        _animator.SetTrigger(_die);
    }

    public void StopAnimation()
    {
        _animator.speed = 0.0f;
    }

    public void ResumeAnimation()
    {
        _animator.speed = 1.0f;
    }

}