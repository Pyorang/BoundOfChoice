using UnityEngine;

public class RerollTicket : ItemBase
{
    public override bool ApplyEffect()
    {
        if (ItemEffectController.Instance.IsEffecting)
        {
            return false;
        }

        ItemEffectController.Instance.UseRerollTicket();

        return true;
    }
}
