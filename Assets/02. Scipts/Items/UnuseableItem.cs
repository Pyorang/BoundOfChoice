using UnityEngine;

public class UnuseableItem : ItemBase
{
    public override bool ApplyEffect()
    {
        return false;
    }
}
