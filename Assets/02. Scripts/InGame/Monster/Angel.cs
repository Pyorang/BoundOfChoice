using UnityEngine;

public class Angel : SingletonBehaviour<Angel>
{
    [Header("레버들")]
    [Space]
    [SerializeField] private LeverController _aLever;
    [SerializeField] private LeverController _bLever;

    [Header("애니메이션 관련")]
    [Space]
    [SerializeField] private Animator _animator;
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    public void StartAnimation()
    {
        _animator.SetTrigger(AttackTrigger);
    }

    public void EnableLevers()
    {
        _aLever.EnableInteract();
        _bLever.EnableInteract();
    }

    public void DisableLevers()
    {
        _aLever.DisableInteract();
        _bLever.DisableInteract();
    }
}
