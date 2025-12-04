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
    [SerializeField] private Vector2 _spawnOffset = new Vector2(1f, 1f);

    public override void Attack(int direction, int additionalDamage)
    {
        if (PlayerMana.Instance.TryUseMana(_magicBoltCost))
        {
            AudioManager.Instance.Play(AudioType.SFX, "MagicBolt");
            GameObject magicBolt = PoolManager.Instance.GetObject(EPoolType.MagicBolt);

            Vector2 directionalSpawnOffset = new Vector2(_spawnOffset.x * direction, _spawnOffset.y);
            Vector2 spawnPosition = (Vector2)transform.position + directionalSpawnOffset;
            magicBolt.GetComponent<ProjectileBase>().Init(spawnPosition, direction, additionalDamage);
        }
    }
}
