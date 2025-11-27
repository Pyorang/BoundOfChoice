using UnityEngine;

public class KnightTrapManager : SingletonBehaviour<KnightTrapManager>
{
    [Header("함정 바인딩")]
    [Space]
    [SerializeField] private Trap_Knight[] _knights;

    private int _activatedKnightsCount = 0;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            ActivateKnight();
        }
        if(Input.GetKeyUp(KeyCode.J))
        {
            DeactivateKnight();
        }

        if(Input.GetKeyUp(KeyCode.K))
        {
            ActivateAll();
        }

        if(Input.GetKeyUp(KeyCode.L))
        {
            DeActivateAll();
        }
    }

    public void ActivateKnight()
    {
        if (_activatedKnightsCount >= _knights.Length) return;

        _activatedKnightsCount++;
        _knights[_activatedKnightsCount - 1].ActivateTrap();
    }

    public void DeactivateKnight()
    {
        if (_activatedKnightsCount <= 0) return;

        _knights[_activatedKnightsCount - 1].DeactivateTrap();
        _activatedKnightsCount--;
    }

    public void ActivateAll()
    {
        foreach(var knight in _knights)
        {
            knight.ActivateTrap();
        }

        _activatedKnightsCount = _knights.Length;
    }

    public void DeActivateAll()
    {
        foreach (var knight in _knights)
        {
            knight.DeactivateTrap();
        }

        _activatedKnightsCount = 0;
    }
}
