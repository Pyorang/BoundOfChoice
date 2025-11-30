using UnityEngine;

public class Choice17 : ChoiceBase
{
    private static readonly float _ASpawnRepeatChance = 0.65f;

    protected override void StepA()
    {
        while(true)
        {
            if(Random.value <= _ASpawnRepeatChance)
            {
                Debug.Log("검사 스켈레톤 한 마리 소환");
            }
            else
            {
                break;
            }
        }
    }

    protected override void StepB()
    {
        Debug.Log("엘리트 스켈레톤 한 마리와 네크로 스켈레톤을 한 마리 소환");
    }
}
