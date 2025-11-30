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
        CanAppear = false;

        for(int i = 0; i < _executeACount + 1; i++)
        {
            ExecuteRemainingA();
        }

        for(int i = 0; i < _executeBCount + 1; i++)
        {
            ExecuteRemainingB();
        }

        _executeACount = Mathf.Max(0, --_executeACount);
        _executeBCount = Mathf.Max(0, --_executeBCount);

        if(_executeACount == 0 && _executeBCount == 0)
        {
            CanAppear = true;
        }
    }
}