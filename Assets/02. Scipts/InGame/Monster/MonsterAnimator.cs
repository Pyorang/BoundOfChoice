using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly int _isMoving = Animator.StringToHash("IsMoving");
    private readonly int _attack = Animator.StringToHash("Attack");
    private readonly int _hurt = Animator.StringToHash("Hurt");

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
}