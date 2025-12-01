using UnityEngine;

public abstract class ChoiceBase
{
    protected bool _canAppear = true;
    public bool CanAppear
    {
        get { return _canAppear; }
        private set
        {
            _canAppear = value;
        }
    }

    protected int _executeACount = 0;
    protected int _executeBCount = 0;

    protected abstract void StepA();
    protected abstract void StepB();

    public virtual void ExecuteA()
    {
        StepA();
        GetNewChoice();
    }

    public virtual void ExecuteB()
    {
        StepB();
        GetNewChoice();
    }

    protected virtual void ExecuteRemainingA()
    {

    }

    protected virtual void ExecuteRemainingB()
    {

    }

    public void GetNewChoice()
    {
        if (MonsterSpawner.Instance.CurrentMonsterCount == 0)
        {
            ChoiceManager.Instance.GetNewChoice();
        }
    }

    public void ProcessLeftTurn()
    {
        if (_executeACount > 0)
        {
            _executeACount--;
        }
        if (_executeBCount > 0)
        {
            _executeBCount--;
        }

        ExecuteRemainingA();
        ExecuteRemainingB();

        if (_executeACount == 0 && _executeBCount == 0)
        {
            CanAppear = true;
        }
        else
        {
            CanAppear = false;
        }
    }
}