using System.Collections;
using UnityEngine;

public class ItemEffectController : SingletonBehaviour<ItemEffectController>
{
    [Header("이펙트 관련 오브젝트들")]
    [Space]
    [SerializeField] private GameObject RerollEffect;
    [SerializeField] private GameObject EliminateEffect;

    [Header("연출 효과 시간")]
    [Space]
    [SerializeField] private float RerollDuration;
    [SerializeField] private float EliminateDuration;

    private bool _isEffecting;
    public bool IsEffecting => _isEffecting;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    public void UseRerollTicket()
    {
        StartCoroutine(StartRerollEffect());
    }

    public void UseEliminateTicket()
    {
        StartCoroutine(StartEliminateEffect());
    }

    private IEnumerator StartRerollEffect()
    {
        _isEffecting = true;
        RerollEffect.SetActive(true);
        AudioManager.Instance.Play(AudioType.SFX, "RerollTicket");

        float timeElapsed = 0;

        while(timeElapsed < RerollDuration)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        ChoiceManager.Instance.GetNewChoice();

        _isEffecting = false;
        RerollEffect.SetActive(false);
    }

    private IEnumerator StartEliminateEffect()
    {
        _isEffecting = true;
        EliminateEffect.SetActive(true);
        AudioManager.Instance.Play(AudioType.SFX, "EliminateTicket");

        float timeElapsed = 0;

        while (timeElapsed < EliminateDuration)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        PoolManager.Instance.ReleaseAllObjects(EPoolType.SkeletonSwordsman);
        PoolManager.Instance.ReleaseAllObjects(EPoolType.SkeletonArbalist);
        PoolManager.Instance.ReleaseAllObjects(EPoolType.SkeletonElite);
        PoolManager.Instance.ReleaseAllObjects(EPoolType.SkeletonNecro);
        PoolManager.Instance.ReleaseAllObjects(EPoolType.BringerOfDeath);

        _isEffecting = false;
        EliminateEffect.SetActive(false);
    }
}
