using UnityEngine;

public class QuickSlotController : MonoBehaviour
{
    [SerializeField] private SlotController[] _slotControllers;

    private void Update()
    {
        UseItemInQuickSlot();
    }

    private void UseItemInQuickSlot()
    {
        for (int i = 0; i < _slotControllers.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                _slotControllers[i].UseItem();
            }
        }
    }
}
