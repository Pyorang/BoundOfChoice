using UnityEngine;

public enum EItemType
{
    None,
    Apple,
    HealthPotion,
    ManaPotion,
    ArrowItem,
    FireMagicScroll,
    IceMagicScroll,
    RerollTicket,
    EliminationTicket,
}

public class ItemSpawner : SingletonBehaviour<ItemSpawner>
{
    [Header("드롭 연출 설정")]
    [Space]
    [SerializeField] private float _dropForce;
    [SerializeField] private float _minYForce;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    public GameObject SpawnItem(EPoolType itemType, Vector3 position)
    {
        GameObject item = PoolManager.Instance.GetObject(itemType);
        item.transform.position = position;

        Rigidbody2D itemRigidbody = item.GetComponent<Rigidbody2D>();

        if (itemRigidbody == null) return item;

        itemRigidbody.linearVelocity = Vector2.zero;
        itemRigidbody.angularVelocity = 0f;

        Vector2 dropDirection = Random.insideUnitCircle.normalized;

        if (dropDirection.y < _minYForce)
        {
            dropDirection.y = _minYForce;
        }

        itemRigidbody.AddForce(dropDirection * _dropForce, ForceMode2D.Impulse);

        return item;
    }
}
