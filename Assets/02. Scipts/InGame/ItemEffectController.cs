using System.Collections;
using UnityEngine;

public class ItemEffectController : SingletonBehaviour<ItemEffectController>
{
    [Header("이펙트 관련 오브젝트들")]
    [Space]
    [SerializeField] private GameObject _rerollEffect;
    [SerializeField] private GameObject _eliminateEffect;

    [Header("연출 효과 시간")]
    [Space]
    [SerializeField] private float _rerollDuration;
    [SerializeField] private float _eliminateDuration;

    private bool _isEffecting;
    public bool IsEffecting => _isEffecting;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    public void UseRerollTicket()
    {
        StartCoroutine(
            StartEffect(_rerollEffect, _rerollDuration, "RerollTicket",
                () => ChoiceManager.Instance.GetNewChoice()
            )
        );
    }

    public void UseEliminateTicket()
    {
        StartCoroutine(
            StartEffect(_eliminateEffect, _eliminateDuration, "EliminateTicket",
                () =>
                {
                    PoolManager.Instance.ReleaseAllObjects(EPoolType.SkeletonSwordsman);
                    PoolManager.Instance.ReleaseAllObjects(EPoolType.SkeletonArbalist);
                    PoolManager.Instance.ReleaseAllObjects(EPoolType.SkeletonElite);
                    PoolManager.Instance.ReleaseAllObjects(EPoolType.SkeletonNecro);
                    PoolManager.Instance.ReleaseAllObjects(EPoolType.BringerOfDeath);
                }
            )
        );
    }

    private IEnumerator StartEffect(GameObject effect, float duration, string audioName, System.Action onComplete)
    {
        _isEffecting = true;
        effect.SetActive(true);
        AudioManager.Instance.Play(AudioType.SFX, audioName);

        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        onComplete?.Invoke();
        _isEffecting = false; 
        effect.SetActive(false);
    }
}
