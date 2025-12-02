using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [Header("아이템 드롭 설정")]
    [Space]
    [SerializeField] private EItemType _dropItem;
    [SerializeField] private int _dropCount;
    [SerializeField] private float _dropChance;

    [Header("드롭 연출 설정")]
    [Space]
    [SerializeField] private float _dropForce;
    [SerializeField] private float _minYForce;

    public void TryDropItem()
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
        GameObject item = ItemSpawner.Instance.CreateItem(_dropItem, dropPosition);

        Rigidbody2D itemRigidbody = item.GetComponent<Rigidbody2D>();

        if (itemRigidbody == null) return;

        Vector2 dropDirection = Random.insideUnitCircle.normalized;

        if (dropDirection.y < _minYForce)
        {
            dropDirection.y = _minYForce;
        }

        itemRigidbody.AddForce(dropDirection * _dropForce, ForceMode2D.Impulse);
    }
}
