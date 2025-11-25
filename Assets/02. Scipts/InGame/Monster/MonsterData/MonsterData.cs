using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterData", menuName = "Monster System/Monster Data")]
public class MonsterData : ScriptableObject
{
    [Header("체력")]
    [SerializeField] private int _maxHealth;

    [Header("공격력")]
    [SerializeField] private int _attackPower;

    [Header("이동 속도")]
    [SerializeField] private float _moveSpeed;

    public int MaxHealth => _maxHealth;
    public int AttackPower => _attackPower;
    public float MoveSpeed => _moveSpeed;
}