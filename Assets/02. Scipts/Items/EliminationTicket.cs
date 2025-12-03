using UnityEngine;

public class EliminationTicket : ItemBase
{
    public override bool ApplyEffect()
    {
        if(ItemEffectController.Instance.IsEffecting)
        {
            return false;
        }

        ItemEffectController.Instance.UseEliminateTicket();

        return true;
    }
}
