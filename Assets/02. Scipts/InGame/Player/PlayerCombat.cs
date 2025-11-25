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
    [SerializeField] private float _attackPower;
    public static event Action<int> OnPowerChanged;

    private PlayerMovement _movement;
    private Dictionary<ECharacterType, CharacterBase> _characters = new Dictionary<ECharacterType, CharacterBase>();
    private ECharacterType _currentCharacter = ECharacterType.Warrior;

    public float AttackPower
    {
        get => _attackPower;
        private set
        {
            _attackPower = value;
            OnPowerChanged?.Invoke((int)_attackPower);
        }
    }

    public float AttackPowerRatio => _attackPower * 0.1f + 1.0f;

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
    }

    private void UseFireBall()
    {
        GameObject fireBallObject = PoolManager.Instance.GetObject(EPoolType.FireBall);
        ProjectileBase projectile = fireBallObject.GetComponent<ProjectileBase>();
        if (projectile == null) return;
        if (projectile.TryConsumeCost() == false) return;
        projectile.Init(this.transform.position, _movement.PlayerDirection, AttackPowerRatio);
    }

    private void GetAttackKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CharacterBase currentCharacter = _characters[_currentCharacter];
            if (currentCharacter.CanAttack())
            {
                currentCharacter.Attack(this.transform.position, AttackPowerRatio, _movement.PlayerDirection);
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
        _characters[_currentCharacter].DrawRange(this.transform.position, _movement.PlayerDirection);
    }
#endif
}
