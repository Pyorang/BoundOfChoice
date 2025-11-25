using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : SingletonBehaviour<ShopUI>
{
    [Header("아이템 구매 수량 선택 창")]
    [Space]
    [SerializeField] private GameObject _itemQuantitySelectionWindow;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemDescriptionText;
    [SerializeField] private Button _removeButton;
    [SerializeField] private TextMeshProUGUI _itemQuantityText;
    [SerializeField] private Button _addButton;
    [SerializeField] private TextMeshProUGUI _totalPriceText;
    [SerializeField] private Button _returnButton;
    [SerializeField] private Button _confirmButton;
    
    private int _currentCount = 1;
    public int CurrentCount
    {
        get { return _currentCount; }
        private set
        {
            _currentCount = Mathf.Max(1, value);
            _removeButton.interactable = _currentCount > 1;

            _itemQuantityText.text = $"{value}";
            _totalPriceText.text = $"{_itemInfo.Price * value}G";
        }
    }

    private ItemBase _selectedItem;
    private ItemModel _itemInfo;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        _itemQuantitySelectionWindow.SetActive(false);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void OnClickCloseUI()
    {
        gameObject.SetActive(false);
    }

    public void OnClickBuyItem(ShopSlot shopslot)
    {
        _itemQuantitySelectionWindow.SetActive(true);
        ShowSelectedItem(shopslot);
    }

    private void ShowSelectedItem(ShopSlot shopslot)
    {
        _selectedItem = shopslot.Item;
        _itemInfo = DataTableManager.Instance.GetItemModel(_selectedItem.GetItemID() - 1);

        CurrentCount = 1;

        _itemNameText.text = _itemInfo.Name;
        _itemImage.sprite = shopslot.Item.ItemImage;
        _itemImage.preserveAspect = true;
        _itemDescriptionText.text = _itemInfo.Description;
    }

    public void OnClickAddButton()
    {
        CurrentCount++;
        // NOTE ; 사는 버튼 비활성화 확인
    }

    public void OnClickRemoveButton()
    {
        CurrentCount--;
        // NOTE ; 사는 버튼 활성화 확인
    }

    public void OnClickConfirmButton()
    {
        // NOTE : 로직
        // 1. 골드 확인
        InventoryUI.Instance.GetItem(_selectedItem, CurrentCount);
        // 3. 골드 차감
        _itemQuantitySelectionWindow.SetActive(false);
    }

    public void OnClickReturnButton()
    {
        _itemQuantitySelectionWindow.SetActive(false);
    }
}