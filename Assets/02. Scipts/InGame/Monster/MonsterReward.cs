using UnityEngine;

public class MonsterReward : MonoBehaviour
{
    [Header("보상 설정")]
    [SerializeField] private int _additionalDamageAmount = 10;
    [SerializeField] private int _additionalSpeed = 3;
    [SerializeField] private int _additionalMaxHealth = 10;
    [SerializeField] private int _goldAmount = 25;

    private bool _shouldGiveReward = false;

    public virtual void GiveReward()
    {
        if(!_shouldGiveReward) return;

        PlayerCombat.Instance.AdditionalPower += _additionalDamageAmount;
        PlayerMovement.Instance.AdditionalMoveSpeed += _additionalSpeed;
        PlayerHealth.Instance.MaxHealth += _additionalMaxHealth;
        GoldManager.Instance.GetGold(_goldAmount);

        _shouldGiveReward = false;
    }

    public void EnableReward()
    {
        _shouldGiveReward = true;
    }
}
