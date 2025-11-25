using UnityEngine;

public abstract class ItemBase : InteractObjectBase
{
    [Header("아이템 타입")]
    [SerializeField] private EItemType _itemType;
    private Sprite _itemImage;
    
    public EItemType ItemType => _itemType;
    public Sprite ItemImage => _itemImage;

    private void Awake()
    {
        _itemImage = GetComponent<SpriteRenderer>().sprite;
    }

    public override void GetItem()
    {
        InventoryUI.Instance.GetItem(this, 1);
        gameObject.SetActive(false);
    }
    public abstract void ApplyEffect();
}

