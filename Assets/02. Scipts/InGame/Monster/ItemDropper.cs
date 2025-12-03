using UnityEngine;

public class ItemDropper : MonsterReward
{
    [Header("아이템 드롭 설정")]
    [Space]
    [SerializeField] private EPoolType _dropItem;
    [SerializeField] private int _dropCount;
    [SerializeField] private float _dropChance;

    public override void GiveReward()
    {
        for (int i = 0; i < _dropCount; ++i)
        {
            DropItem();
        }
    }

    private void DropItem()
    {
        if (Random.value > _dropChance) return;

        Vector2 dropPosition = Vector2.zero;
        dropPosition.x = transform.position.x;
        ItemSpawner.Instance.SpawnItem(_dropItem, dropPosition);
    }
}
