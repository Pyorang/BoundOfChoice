using UnityEngine;

public class HealthPotion : ItemBase
{
    [Header("체력 회복량")]
    [SerializeField] private int _amount;

    public override void ApplyEffect()
    {
        PlayerHealth.Instance.Heal(_amount);
    }
}
