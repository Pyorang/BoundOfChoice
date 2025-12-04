using UnityEngine;

public class ManaPotion : ItemBase
{
    [Header("마나 회복량")]
    [SerializeField] private int _amount;

    public override bool ApplyEffect()
    {
        AudioManager.Instance.Play(AudioType.SFX, "Potion");
        PlayerMana.Instance.RegenerateMana(_amount);
        return true;
    }
}
