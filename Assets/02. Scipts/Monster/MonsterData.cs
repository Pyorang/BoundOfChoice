using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterData", menuName = "Monster System/Monster Data")]
public class MonsterData : ScriptableObject
{
    [Header("체력")]
    [SerializeField] private float _maxHealth;

    [Header("공격력")]
    [SerializeField] private float _attackPower;

    [Header("이동 속도")]
    [SerializeField] private float _moveSpeed;

    public float MaxHealth => _maxHealth;
    public float AttackPower => _attackPower;
    public float MoveSpeed => _moveSpeed;
}