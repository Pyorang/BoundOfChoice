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

    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    [SerializeField] private float _correctionForce;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    public GameObject SpawnItem(EPoolType itemType, Vector3 position)
    {
        GameObject item = PoolManager.Instance.GetObject(itemType);
        item.transform.position = position;

        SpawnEffect(item, position);
        return item;
    }

    private void SpawnEffect(GameObject item, Vector3 position)
    {
        Rigidbody2D itemRigidbody = item.GetComponent<Rigidbody2D>();

        if (itemRigidbody == null) return;

        itemRigidbody.linearVelocity = Vector2.zero;
        itemRigidbody.angularVelocity = 0f;

        Vector2 dropDirection = Random.insideUnitCircle.normalized;

        if (dropDirection.y < _minYForce)
        {
            dropDirection.y = _minYForce;
        }

        if (_maxX < position.x)
        {
            item.transform.position = new Vector3(_maxX, position.y, position.z);
            dropDirection.x -= _correctionForce;
        }

        else if (_minX > position.x)
        {
            item.transform.position = new Vector3(_minX, position.y, position.z);
            dropDirection.x += _correctionForce;
        }

        itemRigidbody.AddForce(dropDirection * _dropForce, ForceMode2D.Impulse);
    }
}
