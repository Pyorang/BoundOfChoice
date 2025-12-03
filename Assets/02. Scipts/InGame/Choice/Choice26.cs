using UnityEngine;

public class Choice26 : ChoiceBase
{
    protected override void StepLeft()
    {
        if(Random.value <= 0.5f)
        {
            KnightTrapManager.Instance.ActivateAll();
        }
        else
        {
            KnightTrapManager.Instance.DeActivateAll();
        }
    }

    protected override void StepRight()
    {
        
    }
}