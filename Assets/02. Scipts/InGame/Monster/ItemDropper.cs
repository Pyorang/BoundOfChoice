using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [Header("아이템 드롭 설정")]
    [Space]
    [SerializeField] private EItemType _dropItem;
    [SerializeField] private float _dropChance;
    
    public void TryDropItem()
    {
        if (Random.value > _dropChance) return;

        Vector2 dropPosition = Vector2.zero;
        dropPosition.x = transform.position.x;
        ItemSpawner.Instance.CreateItem(_dropItem, dropPosition);
    }
}
