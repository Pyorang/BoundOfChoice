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

    protected int _executeLeftCount = 0;
    protected int _executeRightCount = 0;

    protected abstract void StepLeft();
    protected abstract void StepRight();

    public virtual void ExecuteLeft()
    {
        StepLeft();
        GetNewChoice();
    }

    public virtual void ExecuteRight()
    {
        StepRight();
        GetNewChoice();
    }

    protected virtual void ExecuteLeftRemaining()
    {

    }

    protected virtual void ExecuteRightRemaining()
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
        if (_executeLeftCount > 0)
        {
            _executeLeftCount--;
        }
        if (_executeRightCount > 0)
        {
            _executeRightCount--;
        }

        ExecuteLeftRemaining();
        ExecuteRightRemaining();

        if (_executeLeftCount == 0 && _executeRightCount == 0)
        {
            CanAppear = true;
        }
        else
        {
            CanAppear = false;
        }
    }
}