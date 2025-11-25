using UnityEngine;

public class Choice4 : ChoiceBase
{
    public override void Execute1()
    {
        MonsterSpawner.Instance.CreateMonster(0, MonsterSpawner.Instance.transform.position);
        base.Execute1();
    }

    public override void Execute2()
    {
        MonsterSpawner.Instance.CreateMonster(0, MonsterSpawner.Instance.transform.position);
        MonsterSpawner.Instance.CreateMonster(0, MonsterSpawner.Instance.transform.position);
        GoldManager.Instance.GetGold(20);
        base.Execute2();
    }
}
