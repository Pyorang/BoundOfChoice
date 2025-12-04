using UnityEngine;

public class Choice18 : ChoiceBase
{
    protected override void StepLeft()
    {
        GameObject monster = MonsterSpawner.Instance.SpawnMonster(EPoolType.BringerOfDeath);
        MonsterReward reward = monster.GetComponent<MonsterReward>();
        reward.EnableReward();

    }

    protected override void StepRight()
    {
        
    }
}
