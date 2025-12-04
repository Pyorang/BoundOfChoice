using UnityEngine;

public class Archer : CharacterBase
{
    [Header("화살 위치 보정")]
    [Space]
    [SerializeField] private Vector2 _spawnOffset = new Vector2(1f, -0.5f);

    public override void Attack(int direction, int additionalDamage)
    {
        AudioManager.Instance.Play(AudioType.SFX, "ShootArrow");
        if (!InventoryUI.Instance.TryConsumeItem(EItemType.ArrowItem)) return;

        GameObject arrowObject = PoolManager.Instance.GetObject(EPoolType.ArrowProjectile);
        Vector2 directionalSpawnOffset = new Vector2(_spawnOffset.x * direction, _spawnOffset.y);
        Vector2 spawnPosition = (Vector2)transform.position + directionalSpawnOffset;
        arrowObject.GetComponent<ProjectileBase>().Init(spawnPosition, direction, additionalDamage);
    }
}
