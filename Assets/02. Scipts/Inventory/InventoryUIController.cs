using UnityEngine;

public class InventoryUIController : SingletonBehaviour<InventoryUIController>
{
    [SerializeField] GameObject _slotContent;
    private SlotController[] _slots;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();

        _slots = _slotContent.GetComponentsInChildren<SlotController>(true);
        gameObject.SetActive(false);
    }

    public void ToggleInventory()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void GetItem(ItemBase item, int count)
    {
        foreach (SlotController slot in _slots)
        {
            if (slot.CompareItem(item.ItemType))
            {
                slot.AddItem(count);
                return;
            }
        }
        foreach (SlotController slot in _slots)
        {
            if (slot.IsEmpty)
            {
                slot.SetSlot(item, count);
                return;
            }
        }
    }
}
