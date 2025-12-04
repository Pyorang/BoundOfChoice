using UnityEngine;

public class HealthPotion : ItemBase
{
    [Header("체력 회복량")]
    [SerializeField] private int _amount;

    public override bool ApplyEffect()
    {
        AudioManager.Instance.Play(AudioType.SFX, "Potion");
        PlayerHealth.Instance.Heal(_amount);
        return true;
    }
}
