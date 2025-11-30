using UnityEngine;

public abstract class BaseMonster : MonoBehaviour
{
    protected MonsterStats _stats;

    protected const string PlayerTag = "Player";

    private void Awake()
    {
        Init();
    }

    protected virtual void Init() 
    {
        _stats = GetComponent<MonsterStats>();
    }
}
