using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float _attackPower;
    public static event Action<int> OnPowerChanged;

    private PlayerMovement _movement;
    private CharacterBase[] _characters;
    private int _currentCharacter = 0;

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
        _characters = Resources.LoadAll<CharacterBase>("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _characters[_currentCharacter].Attack(this.transform.position, AttackPower, _movement.PlayerDirection);
        }
    }

    private void OnDrawGizmos()
    {
        if (_characters == null || _characters.Length == 0) return;
        _characters[_currentCharacter].DrawRange(this.transform.position, _movement.PlayerDirection);
    }
}
