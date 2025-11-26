using UnityEngine;

public abstract class ItemBase : InteractObjectBase
{
    [Header("아이템 타입")]
    [SerializeField] private EItemType _itemType;
    private ItemModel _itemModel;

    [Header("아이템 이미지")]
    [SerializeField] private SpriteRenderer _itemImage;
    
    public EItemType ItemType => _itemType;
    public Sprite ItemImage => _itemImage.sprite;
    public ItemModel ItemInfo => _itemModel;

    private void Awake()
    {
        ItemModelInit();
    }

    private void ItemModelInit()
    {
        _itemModel = DataTableManager.Instance.GetItemModel(GetItemID() - 1);
    }
    public int GetItemID()
    {
        return (int)_itemType;
    }   

    public override void GetItem()
    {
        InventoryUI.Instance.GetItem(this, 1);
        gameObject.SetActive(false);
    }
    public abstract bool ApplyEffect();
}