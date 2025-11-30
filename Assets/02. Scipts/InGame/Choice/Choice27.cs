using UnityEngine;

public class Choice27 : ChoiceBase
{
    protected override void StepA()
    {
        if(Random.value <= 0.5f)
        {
            GoldManager.Instance.GetGold(GoldManager.Instance.Gold);
        }
        else
        {
            GoldManager.Instance.UseGold((int)(GoldManager.Instance.Gold / 2));
        }
    }

    protected override void StepB()
    {
        if (Random.value <= 0.5f)
        {
            PlayerHealth.Instance.Heal(PlayerHealth.Instance.Health);
        }
        else
        {
            PlayerHealth.Instance.TakeDamage((int)(PlayerHealth.Instance.Health / 2));
        }
    }
}
