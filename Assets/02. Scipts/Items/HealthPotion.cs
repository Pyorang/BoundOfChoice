using UnityEngine;

public class HealthPotion : BaseItem
{
    [Header("체력 회복량")]
    [SerializeField] private int _amount;

    protected override void ApplyEffect()
    {
        
    }
}
