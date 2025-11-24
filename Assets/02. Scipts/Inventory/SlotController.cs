using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    private ItemBase _item;
    private int _itemCount;

    [SerializeField] private Image _itemImageUI;
    [SerializeField] private TextMeshProUGUI _itemCountTextUI;


    private void Awake()
    {
        _itemCount = 0;
        gameObject.SetActive(false);
    }

    public void AddSlot(ItemBase item, int count = 1)
    {
        gameObject.SetActive(true);
        _item = item;
        _itemCount = count;
        _itemImageUI.sprite = item.ItemImage;
        _itemCountTextUI.text = _itemCount.ToString();
    }

    public void AddItem()
    {
        ++_itemCount;
        _itemCountTextUI.text = _itemCount.ToString();
    }

    public void UseItem()
    {
        _item.ApplyEffect();
        --_itemCount;
        _itemCountTextUI.text = _itemCount.ToString();

        if (_itemCount > 0) return;
        ClearSlot();
    }

    private void ClearSlot()
    {
        _item = null;
        _itemImageUI.sprite = null;
        gameObject.SetActive(false);
    }

    public bool CompareItem(EItemType itemType)
    {
        if(_item== null) return false;
        return _item.ItemType.Equals(itemType);
    }
}
