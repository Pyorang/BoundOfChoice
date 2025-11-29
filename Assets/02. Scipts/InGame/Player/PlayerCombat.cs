using System;
using System.Collections.Generic;
using UnityEngine;

public enum ECharacterType
{
    Warrior,
    Archer,
    Mage
}

[RequireComponent(typeof(PlayerMovement))]
public class PlayerCombat : SingletonBehaviour<PlayerCombat>
{

    [Header("공격력")]
    [Space]
    [SerializeField] private int _additionalDamage;

    public int AdditionalPower
    {
        get => _additionalDamage;
        private set
        {
            _additionalDamage = value;
            OnPowerChanged?.Invoke(_additionalDamage);
        }
    }

    public static event Action<int> OnPowerChanged;

    [Header("동료들")]
    [Tooltip("첫 번쨰 동료를 제외한 나머지 동료들의 오브젝트들은 OFF 해주세요")]
    [Space]
    [SerializeField] private CharacterBase[] _partners;
    private ECharacterType _currentCharacter = ECharacterType.Warrior;
    public ECharacterType CurrentCharacter => _currentCharacter;

    [SerializeField] private SwitchEffect _switchEffect;

    private PlayerAnimator _playerAnimator;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        _playerAnimator = GetComponent<PlayerAnimator>();
        base.Init();
    }

    private void Start()
    {
        foreach (CharacterBase partner in _partners)
        {
            if(partner.GoingWith == false)
            {
                partner.DeactivateCharacter();
            }
        }
    }

    private void Update()
    {
        if(!PlayerHealth.Instance.IsDeath)
        {
            GetCharacterChangeInput();
            GetAttackKeyInput();
        }
    }

    private void GetCharacterChangeInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _partners[(int)_currentCharacter].DeactivateCharacter();

            GetNextCharacter();

            _partners[(int)_currentCharacter].ActivateCharacter();
            _playerAnimator.ChangeAnimatorController((int)_currentCharacter);

            _switchEffect.ProcessSwitchEffect();
        }
    }

    private void GetAttackKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !PlayerMovement.Instance.Moving)
        {
            _playerAnimator.PlayAttackAnimation();
        }
    }

    public void Attack()
    {
        CharacterBase currentCharacter = _partners[(int)_currentCharacter];
        currentCharacter.Attack(PlayerMovement.Instance.PlayerDirection, AdditionalPower);
    }

    private void GetNextCharacter()
    {
        ECharacterType nextCharacter = (ECharacterType)(((int)_currentCharacter + 1) % _partners.Length);
        
        while(true)
        {
            if (_partners[(int)nextCharacter].GoingWith == false)
            {
                nextCharacter = (ECharacterType)(((int)nextCharacter + 1) % _partners.Length);
            }

            else
            {
                _currentCharacter = nextCharacter;
                break;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || PlayerMovement.Instance == null) return;
        if (_partners == null || _partners.Length == 0) return;
        _partners[(int)_currentCharacter].DrawRange(this.transform.position, PlayerMovement.Instance.PlayerDirection);
    }
#endif
}
