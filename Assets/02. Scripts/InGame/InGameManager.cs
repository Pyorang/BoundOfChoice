using UnityEngine;

public class InGameManager : SingletonBehaviour<InGameManager>
{
    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    private void Start()
    {
        AudioManager.Instance.Play(AudioType.BGM,"InGame");
    }
}
