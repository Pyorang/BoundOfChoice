using UnityEngine;

public class LeverController : InteractObjectBase
{
    [Header("왼쪽 레버")]
    [SerializeField] private bool _isLeft;

    private bool canInteract = false;

    private Animator _animator;

    private readonly int _pull = Animator.StringToHash("Pull");
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void GetItem()
    {
        if(canInteract)
        {
            Angel.Instance.DisableLevers();
            _animator.SetTrigger(_pull);
            AudioManager.Instance.Play(AudioType.SFX, "Lever");
            CameraController.Instance.StartShake();
            ChoiceManager.Instance.IsLeftChoice = _isLeft;
            Angel.Instance.StartAnimation();
        }
    }

    public void EnableInteract()
    {
        canInteract = true;
    }

    public void DisableInteract()
    {
        canInteract = false;
    }
}