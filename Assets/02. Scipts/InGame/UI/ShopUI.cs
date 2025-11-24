using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : BaseUI
{
    [Header("아이템 이미지")]
    [Tooltip("순서대로 넣어주세요.")]
    [Space]
    [SerializeField] private Image[] _itemImages;

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
            _totalPriceText.text = $"{ _selectedItem.Price * value}G";
        }
    }

    private ItemModel _selectedItem;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        _itemQuantitySelectionWindow.SetActive(false);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void OnClickBuyItem(int index)
    {
        _itemQuantitySelectionWindow.SetActive(true);
        ShowSelectedItem(index);
    }

    private void ShowSelectedItem(int index)
    {
        _selectedItem = DataTableManager.Instance.GetItemModel(index);

        CurrentCount = 1;

        _itemNameText.text = _selectedItem.Name;
        _itemImage.sprite = _itemImages[index].sprite;
        _itemImage.preserveAspect = true;
        _itemDescriptionText.text = _selectedItem.Description;
    }

    public void OnClickAddButton()
    {
        CurrentCount++;
    }

    public void OnClickRemoveButton()
    {
        CurrentCount--;
    }

    public void OnClickConfirmButton()
    {
        // NOTE : 로직
        // 1. 골드 확인
        // 2. 아이템 추가 및 골드 차감
        _itemQuantitySelectionWindow.SetActive(false);
    }

    public void OnClickReturnButton()
    {
        _itemQuantitySelectionWindow.SetActive(false);
    }
}