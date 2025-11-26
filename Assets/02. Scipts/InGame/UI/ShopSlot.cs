using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [Header("표시할 아이템")]
    [Space]
    [SerializeField] private GameObject _itemPrefab;
    private ItemBase _item;

    [Header("관련 UI")]
    [Space]
    [SerializeField] private Image _itemImageUI;
    [SerializeField] private TextMeshProUGUI _itemNameTextUI;

    public ItemBase Item => _item;

    private void Start()
    {
        InitShopSlot();
    }

    private void InitShopSlot()
    {
        if(_itemPrefab != null)
        {
            _item = _itemPrefab.GetComponent<ItemBase>();
        }

        if (_item == null)
        {
            Debug.LogError($"{nameof(ShopSlot)}: 표시할 아이템이 설정되지 않았습니다.");
            return;
        }
        _itemImageUI.sprite = _item.ItemImage;
        _itemImageUI.preserveAspect = true;
        _itemNameTextUI.text = $"{DataTableManager.Instance.GetItemModel(_item.GetItemID() - 1).Price}G";
    }

    public void OnClickPriceButton()
    {
        if(_itemPrefab != null)
        {
            ShopUI.Instance.OnClickBuyItem(this);
        }
    }
}
