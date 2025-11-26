using UnityEngine;

public class Choice3 : ChoiceBase
{
    public override void Execute1()
    {
        int RandomValue = Random.Range(1, 101);
        if(RandomValue <= 90)
        {
            PlayerHealth.Instance.TakeDamage(30);
        }
        base.Execute1();
    }

    public override void Execute2()
    {
        int RandomValue = Random.Range(1, 101);
        if (RandomValue <= 10)
        {
            PlayerHealth.Instance.Die();
        }
        base.Execute2();
    }
}
