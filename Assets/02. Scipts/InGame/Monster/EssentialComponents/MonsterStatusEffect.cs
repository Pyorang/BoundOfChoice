using System;
using System.Collections;
using UnityEngine;

public class MonsterStatusEffect : MonoBehaviour
{
    public event Action<int> OnDotDamageTick;
    public event Action OnBindStart;
    public event Action OnBindEnd;

    private Coroutine _bindCoroutine;
    private Coroutine _dotDamageCoroutine;

    public void ApplyDotDamage(int damage, float duration, float interval)
    {
        if (_dotDamageCoroutine != null)
        {
            StopCoroutine(_dotDamageCoroutine);
        }
        _dotDamageCoroutine = StartCoroutine(ProcessDotDamage(damage, duration, interval));
    }

    public void ApplyBind(float duration)
    {
        if (_bindCoroutine != null)
        {
            StopCoroutine(_bindCoroutine);
        }
        _bindCoroutine = StartCoroutine(ProcessBind(duration));
    }

    private IEnumerator ProcessDotDamage(int damage, float duration, float interval)
    {
        float elapsedTime = 0.0f;
        WaitForSeconds waitDelay = new WaitForSeconds(interval);

        while (elapsedTime < duration)
        {
            yield return waitDelay;

            OnDotDamageTick?.Invoke(damage);

            elapsedTime += interval;
        }

        _dotDamageCoroutine = null;
    }

    private IEnumerator ProcessBind(float duration)
    {
        OnBindStart?.Invoke();

        yield return new WaitForSeconds(duration);

        OnBindEnd?.Invoke();

        _bindCoroutine = null;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _bindCoroutine = null;
        _dotDamageCoroutine = null;
    }
}
