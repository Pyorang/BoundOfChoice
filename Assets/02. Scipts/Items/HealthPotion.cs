using UnityEngine;

public class HealthPotion : InteractObjectBase
{
    [Header("체력 회복량")]
    [SerializeField] private int _amount;

    public override void Execute()
    {
        PlayerHealth.Instance.Heal(_amount);
        gameObject.SetActive(false);
    }
}
