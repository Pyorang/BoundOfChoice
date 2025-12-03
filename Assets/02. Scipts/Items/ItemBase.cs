using UnityEngine;

public abstract class ItemBase : InteractObjectBase
{
    [Header("아이템 타입")]
    [SerializeField] private EItemType _itemType;
    [SerializeField] private EPoolType _itemPoolType;
    private ItemModel _itemModel;

    [Header("아이템 이미지")]
    [SerializeField] private SpriteRenderer _itemImage;

    [Header("아이템 수량 표시")]
    [SerializeField] private bool _showItemCount;
    
    public EItemType ItemType => _itemType;
    public Sprite ItemImage => _itemImage.sprite;
    public bool ShowItemCount => _showItemCount;
    public ItemModel ItemInfo => _itemModel;

    private static readonly string s_itemSound = "GainItem";

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
        AudioManager.Instance.Play(AudioType.SFX, s_itemSound);
        InventoryUI.Instance.GetItem(this, 1);
        PoolManager.Instance.ReleaseObject(_itemPoolType, gameObject);
    }

    public abstract bool ApplyEffect();
}
