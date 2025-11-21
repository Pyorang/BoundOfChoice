using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterData", menuName = "Monster System/Monster Data")]
public class MonsterData : ScriptableObject
{
    [Header("몬스터 이름")]
    [SerializeField] private EMonsterType _monsterType;

    [Header("체력")]
    [SerializeField] private float _maxHealth;

    [Header("공격력")]
    [SerializeField] private float _attackPower;

    [Header("이동 속도")]
    [SerializeField] private float _moveSpeed;

    public EMonsterType MonsterType => _monsterType;
    public float MaxHealth => _maxHealth;
    public float AttackPower => _attackPower;
    public float MoveSpeed => _moveSpeed;
}