using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotController : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    private ItemBase _item;
    private int _itemCount;

    private Vector2 _originPosition;
    private Vector2 _beginDragPosition;

    [SerializeField] private Image _itemImageUI;
    [SerializeField] private TextMeshProUGUI _itemCountTextUI;

    public bool IsEmpty => _item == null;

    private void Awake()
    {
        ClearSlot();
    }

    public void SetSlot(ItemBase item, int count = 1)
    {
        if (item == null || count <= 0)
        {
            ClearSlot();
            return;
        }

        SetColor(1);
        _item = item;
        _itemCount = count;
        _itemImageUI.sprite = item.ItemImage;
        _itemCountTextUI.text = _itemCount.ToString();
    }

    public void AddItem(int count)
    {
        _itemCount += count;
        _itemCountTextUI.text = _itemCount.ToString();
    }

    public void UseItem()
    {
        if (IsEmpty) return;

        _item.ApplyEffect();
        --_itemCount;
        _itemCountTextUI.text = _itemCount.ToString();

        if (_itemCount > 0) return;
        ClearSlot();
    }

    public void ClearSlot()
    {
        SetColor(0);
        _item = null;
        _itemCount = 0;
        _itemImageUI.sprite = null;
        _itemCountTextUI.text = null;
    }

    public bool CompareItem(EItemType itemType)
    {
        if (_item == null) return false;
        return _item.ItemType.Equals(itemType);
    }

    private void SwapSlot(SlotController slot)
    {        
        ItemBase item = _item;
        int count = _itemCount;
        
        SetSlot(slot._item, slot._itemCount);
        slot.SetSlot(item, count);
    }

    private void MergeSlot(SlotController slot)
    {
        AddItem(slot._itemCount);
        slot.ClearSlot();
    }

    private void SetColor(float alpha)
    {
        Color color = _itemImageUI.color;
        color.a = alpha;
        _itemImageUI.color = color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsEmpty) return;

        _beginDragPosition = eventData.position;
        _originPosition = transform.position;
        DragSlot.Instance.BeginDrag(_itemImageUI.sprite);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 moveOffset = eventData.position - _beginDragPosition;
        DragSlot.Instance.Drag(_originPosition + moveOffset);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.Instance.EndDrag();
    }

    public void OnDrop(PointerEventData eventData)
    {
        SlotController draggedSlot = eventData.pointerDrag.GetComponent<SlotController>();
        if (draggedSlot == null || draggedSlot == this || draggedSlot.IsEmpty) return;

        if (!IsEmpty && draggedSlot.CompareItem(_item.ItemType))
        {
            MergeSlot(draggedSlot);
        }
        else
        {
            SwapSlot(draggedSlot);
        }
    }
}
