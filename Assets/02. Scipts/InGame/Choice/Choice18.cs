using UnityEngine;

public class Choice18 : ChoiceBase
{
    protected override void StepLeft()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.BringerOfDeath);
    }

    protected override void StepRight()
    {
        
    }
}
