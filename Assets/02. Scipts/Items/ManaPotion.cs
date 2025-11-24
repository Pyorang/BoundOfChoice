using UnityEngine;

public class ManaPotion : InteractObjectBase
{
    [Header("마나 회복량")]
    [SerializeField] private int _amount;

    public override void Execute()
    {
        PlayerMana.Instance.RegenerateMana(_amount);
        gameObject.SetActive(false);
    }
}
