using UnityEngine;

public class ManaPotion : BaseItem
{
    [Header("마나 회복량")]
    [SerializeField] private int _amount;
    protected override void ApplyEffect()
    {

    }
}
