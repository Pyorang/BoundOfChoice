using System;
using System.Collections.Generic;
using UnityEngine;

public enum ECharacterType
{
    Warrior,
    Archer,
    Mage
}

public enum ESkillType
{
    MagicBolt,
    FireBall,
    IceAge
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

    public void OnAttack()
    {
        CharacterBase currentCharacter = _characters[_currentCharacter];
        if (currentCharacter.CanAttack())
        {
            currentCharacter.Attack(_movement.PlayerDirection, AttackPower);
            currentCharacter.ResetAttackCooldown();
        }
    }

    public void ChangeCharacter()
    {
        int characterCount = _characters.Count;
        int nextCharacter = ((int)_currentCharacter + 1) % characterCount;
        ECharacterType nextType = (ECharacterType)nextCharacter;

        _characters[_currentCharacter].DeactivateCharacter();
        _currentCharacter = nextType;
        _characters[_currentCharacter].ActivateCharacter();
    }

    public void UseFireBall()
    {
        _characters[_currentCharacter].
            UseSkill(ESkillType.FireBall, _movement.PlayerDirection, AttackPower);
    }

    public void UseIceAge()
    {
        _characters[_currentCharacter].
            UseSkill(ESkillType.IceAge, _movement.PlayerDirection, AttackPower);
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
