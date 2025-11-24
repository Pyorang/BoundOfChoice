using System;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerType
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
    private Dictionary<EPlayerType, CharacterBase> _characters = new Dictionary<EPlayerType, CharacterBase>();
    private EPlayerType _currentCharacter = EPlayerType.Warrior;

    public float AttackPower
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
            string className = character.GetType().Name;
            if (Enum.TryParse<EPlayerType>(className, out var playerType))
            {
                _characters.Add(playerType, character);
            }
        }
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
            ChangeCharacter(EPlayerType.Warrior);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeCharacter(EPlayerType.Archer);
        }
    }

    private void GetAttackKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_characters[_currentCharacter].CanAttack())
            {
                _characters[_currentCharacter].
                    Attack(this.transform.position, AttackPower, _movement.PlayerDirection);
            }
        }
    }

    private void ChangeCharacter(EPlayerType playerType)
    {
        if (_currentCharacter == playerType) return;
        _currentCharacter = playerType;
    }

    private void OnDrawGizmos()
    {
        if (_characters == null || _characters.Count == 0) return;
        _characters[_currentCharacter].DrawRange(this.transform.position, _movement.PlayerDirection);
    }
}
