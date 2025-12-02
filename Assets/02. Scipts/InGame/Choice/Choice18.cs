using UnityEngine;

public class Choice18 : ChoiceBase
{
    protected override void StepA()
    {
        MonsterSpawner.Instance.SpawnMonster(EPoolType.BringerOfDeath);
        // NOTE : 얘 죽을 때 보상 얻게 만들어야 함.
    }

    protected override void StepB()
    {
        
    }
}
