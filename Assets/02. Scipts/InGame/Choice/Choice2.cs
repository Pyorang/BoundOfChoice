using UnityEngine;

public class Choice2 : ChoiceBase
{
    public override void Execute1()
    {
        MonsterSpawner.Instance.CreateMonster(0, MonsterSpawner.Instance.transform.position);
        base.Execute1();
    }

    public override void Execute2()
    {
        ItemSpawner.Instance.CreateItem(EItemType.HealthPotion, ItemSpawner.Instance.transform.position);
        base.Execute2();
    }
}
