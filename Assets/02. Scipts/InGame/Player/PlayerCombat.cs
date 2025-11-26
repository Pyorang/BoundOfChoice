using System;
using System.Collections.Generic;
using UnityEngine;

public enum ECharacterType
{
    Warrior,
    Archer,
    Mage
}

public class PlayerCombat : MonoBehaviour
{
    private PlayerMovement _movement;
    private Dictionary<ECharacterType, CharacterBase> _characters = new Dictionary<ECharacterType, CharacterBase>();
    private ECharacterType _currentCharacter = ECharacterType.Warrior;
    public static event Action<int> OnPowerChanged;

    [Header("공격력")]
    [Space]
    [SerializeField] private int _attackPower;
    [SerializeField] private int _increaseDamagePerPower;

    [Header("스킬")]
    [Space]
    [SerializeField] private IceAge _iceAgeSkill;

    public int AttackPower
    {
        get => _attackPower;
        private set
        {
            _attackPower = value;
            OnPowerChanged?.Invoke((int)_attackPower);
        }
    }

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        CharacterBase[] characters = GetComponentsInChildren<CharacterBase>();
        foreach (CharacterBase character in characters)
        {
            _characters.Add(character.CharacterType, character);
            character.DeactivateCharacter();
        }
        _characters[_currentCharacter].ActivateCharacter();
    }

    private void Update()
    {
        GetCharacterChangeInput();
        GetAttackKeyInput();
    }

    private void GetCharacterChangeInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeCharacter(ECharacterType.Warrior);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeCharacter(ECharacterType.Archer);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeCharacter(ECharacterType.Mage);
        }

        // Note : Test 코드 이후 서적 사용에 따라 발사
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_currentCharacter != ECharacterType.Mage) return;
            UseFireBall();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (_currentCharacter != ECharacterType.Mage) return;
            _iceAgeSkill.UseSkill(this.transform.position, _movement.PlayerDirection, AttackPower);
        }
    }

    private void UseFireBall()
    {
        GameObject fireBallObject = PoolManager.Instance.GetObject(EPoolType.FireBall);
        ProjectileBase projectile = fireBallObject.GetComponent<ProjectileBase>();
        if (projectile == null) return;
        if (!projectile.TryConsumeCost()) return;
        projectile.Init(this.transform.position, _movement.PlayerDirection, AttackPower);
    }

    private void GetAttackKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CharacterBase currentCharacter = _characters[_currentCharacter];
            if (currentCharacter.CanAttack())
            {
                currentCharacter.Attack(this.transform.position, AttackPower, _movement.PlayerDirection);
                currentCharacter.ResetAttackCooldown();
            }
        }
    }

    private void ChangeCharacter(ECharacterType playerType)
    {
        if (_currentCharacter == playerType) return;
        _characters[_currentCharacter].DeactivateCharacter();
        _currentCharacter = playerType;
        _characters[_currentCharacter].ActivateCharacter();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || _movement == null) return;
        if (_characters == null || _characters.Count == 0) return;
        if (_currentCharacter == ECharacterType.Warrior)
        {
            _characters[_currentCharacter].DrawRange(this.transform.position, _movement.PlayerDirection);
        }
        else if (_currentCharacter == ECharacterType.Mage)
        {
            _iceAgeSkill?.DrawRange(this.transform.position, _movement.PlayerDirection);
        }
    }
#endif
}
