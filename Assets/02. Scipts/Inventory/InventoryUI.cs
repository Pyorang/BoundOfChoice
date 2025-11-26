using UnityEngine;

public class InventoryUI : SingletonBehaviour<InventoryUI>
{
    [SerializeField] GameObject _slotContent;
    private SlotController[] _slots;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    private void Start()
    {
        _slots = _slotContent.GetComponentsInChildren<SlotController>(true);
        gameObject.SetActive(false);
    }

    public void ClearInventory()
    {
        foreach (SlotController slot in _slots)
        {
            slot.ClearSlot();
        }
    }

    public void ToggleInventory()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void GetItem(ItemBase item, int count)
    {
        SlotController emptySlot = null;
        foreach (SlotController slot in _slots)
        {
            if (slot.CompareItem(item.ItemType))
            {
                slot.AddItem(count);
                return;
            }
            if (emptySlot == null && slot.IsEmpty)
            {
                emptySlot = slot;
            }
        }

        if (emptySlot != null)
        {
            emptySlot.SetSlot(item, count);
            return;
        }
    }

    public bool TryConsumeItem(EItemType itemType)
    {
        foreach (SlotController slot in _slots)
        {
            if (slot.IsEmpty || !slot.CompareItem(itemType)) continue;

            slot.ConsumeItem();
            return true;
        }
        return false;
    }
}
