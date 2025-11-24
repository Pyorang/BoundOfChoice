using UnityEngine;

public class InventoryUIController : SingletonBehaviour<InventoryUIController>
{
    private bool _inventoryActived = false;

    [SerializeField] GameObject _slotContent;
    private SlotController[] _slots;
    private int _slotIndex;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();

        _slots = _slotContent.GetComponentsInChildren<SlotController>(true);
        _slotIndex = 0;
        gameObject.SetActive(false);
    }

    public void ToggleInventory()
    {
        _inventoryActived = !_inventoryActived;
        gameObject.SetActive(_inventoryActived);
    }

    public void GetItem(ItemBase item, int count)
    {
        foreach (SlotController slot in _slots)
        {
            if (slot.CompareItem(item.ItemType))
            {
                slot.AddItem();
                return;
            }
        }
        _slots[_slotIndex++].AddSlot(item, count);
    }
}
