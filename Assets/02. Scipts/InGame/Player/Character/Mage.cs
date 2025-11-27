using System.Collections.Generic;
using UnityEngine;

public enum ESkillType
{
    FireBall,
    IceAge
}

public class Mage : CharacterBase
{
    [Header("공격 마나 비용")]
    [Space]
    [SerializeField] private int _magicBoltCost = 20;

    [Header("스킬 위치 보정")]
    [Space]
    [SerializeField] private Vector2 _spawnOffset = new Vector2(1f, 0f);
    public Vector2 SpawnOffset => _spawnOffset;

    public override void Attack(int direction, int additionalDamage)
    {
        if (PlayerMana.Instance.TryUseMana(_magicBoltCost))
        {
            GameObject magicBolt = PoolManager.Instance.GetObject(EPoolType.MagicBolt);
            magicBolt.GetComponent<ProjectileBase>().Init((Vector2)transform.position + _spawnOffset * direction, direction, additionalDamage);
        }
    }
}
