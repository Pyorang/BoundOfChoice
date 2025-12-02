using System.Collections;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [Header("주문 공격 설정")]
    [Space]
    [SerializeField] private float _castInterval;

    private MonsterAnimator _animator;
    private WaitForSeconds _waitInterval;
    private Coroutine _castCoroutine;

    private void Awake()
    {
        _animator = GetComponent<MonsterAnimator>();
        _waitInterval = new WaitForSeconds(_castInterval);
    }

    private void OnEnable()
    {
        _castCoroutine = StartCoroutine(CastCoroutine());
    }

    private void OnDisable()
    {
        if (_castCoroutine == null) return;

        StopCoroutine(_castCoroutine);
        _castCoroutine = null;
    }

    private void Cast()
    {
        _animator.PlaySpecialAttackAnimation();
    }

    private IEnumerator CastCoroutine()
    {
        while (true)
        {
            yield return _waitInterval;
            Cast();
        }
    }
}
