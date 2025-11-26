using UnityEngine;

public class Choice5 : ChoiceBase
{
    public override void Execute1()
    {
        int RandomValue = Random.Range(1, 101);
        if (RandomValue <= 50)
        {
            PlayerHealth.Instance.TakeDamage(30);
        }
        else
        {
            PlayerHealth.Instance.Heal(25);
        }
        base.Execute1();
    }

    public override void Execute2()
    {
        base.Execute2();
    }
}