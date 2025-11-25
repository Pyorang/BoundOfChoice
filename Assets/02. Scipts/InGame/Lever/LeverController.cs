using UnityEngine;

public class LeverController : InteractObjectBase
{
    [Header("왼쪽 레버")]
    [SerializeField] private bool _isLeft;

    [Header("레버 연출 설정")]
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakePower;

    private Animator _animator;

    private readonly int _pull = Animator.StringToHash("Pull");
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void GetItem()
    {
        _animator.SetTrigger(_pull);
        CameraController.Instance.StartShake(_shakeDuration, _shakePower);
        // NOTE : 카메라 줌인을 구현한다.
        ChoiceManager.Instance.ExecuteChoice(_isLeft);
    }
}
