using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : SingletonBehaviour<InventoryUI>
{
    [SerializeField] private GameObject _slotContent;
    [SerializeField] private SlotController[] _quickSlots;
    private List<SlotController> _slots;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    private void Start()
    {
        SlotController[] slots = _slotContent.GetComponentsInChildren<SlotController>(true);
        _slots = new List<SlotController>(slots.Length + _quickSlots.Length);
        _slots.AddRange(slots);
        _slots.AddRange(_quickSlots);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
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
