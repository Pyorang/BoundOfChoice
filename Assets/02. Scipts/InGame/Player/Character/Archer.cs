using UnityEngine;

public class Archer : CharacterBase
{
    [Header("화살 위치 보정")]
    [Space]
    [SerializeField] private Vector2 _spawnOffset = new Vector2(1f, 0f);

    public override void Attack(int direction, int additionalDamage)
    {
        AudioManager.Instance.Play(AudioType.SFX, "ShootArrow");
        if (!InventoryUI.Instance.TryConsumeItem(EItemType.ArrowItem)) return;

        GameObject arrowObject = PoolManager.Instance.GetObject(EPoolType.Arrow);
        arrowObject.GetComponent<ProjectileBase>().Init((Vector2)transform.position + _spawnOffset * direction, direction, additionalDamage);
    }
}
