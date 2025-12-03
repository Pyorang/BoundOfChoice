using UnityEngine;

public class MonsterReward : MonoBehaviour
{
    [Header("보상 설정")]
    [SerializeField] private int _additionalDamageAmount = 10;
    [SerializeField] private int _additionalSpeed = 3;
    [SerializeField] private int _additionalMaxHealth = 10;
    [SerializeField] private int _goldAmount = 25;

    public virtual void GiveReward()
    {
        PlayerCombat.Instance.AdditionalPower += _additionalDamageAmount;
        PlayerMovement.Instance.AdditionalMoveSpeed += _additionalSpeed;
        PlayerHealth.Instance.MaxHealth += _additionalMaxHealth;
        GoldManager.Instance.GetGold(_goldAmount);
    }
}
