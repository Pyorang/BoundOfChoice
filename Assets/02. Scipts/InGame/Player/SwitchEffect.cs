using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class SwitchEffect : MonoBehaviour
{
    [Header("교체 이펙트 설정")]
    [Space]
    [SerializeField] private float _targetIntensity;
    [SerializeField] private float _effectDuration;

    [Header("Light2D 컴포넌트")]
    [Space]
    [SerializeField] private Light2D _light;

    private IEnumerator _effectCoroutine;

    private void Awake()
    {
        if (_light == null)
        {
            _light = GetComponent<Light2D>();
        }
    }

    public void ProcessSwitchEffect()
    {
        if(_effectCoroutine != null )
        {
            StopCoroutine(_effectCoroutine);
            _light.intensity = 0;
        }

        _effectCoroutine = SwitchingEffect();
        StartCoroutine(_effectCoroutine);
    }

    private IEnumerator SwitchingEffect()
    {
        float timeElapsed = 0;
        float halfDuration = _effectDuration / 2;

        while (timeElapsed < _effectDuration)
        {
            float pingPongValue = Mathf.PingPong(timeElapsed, halfDuration);
            _light.intensity = Mathf.Lerp(0, _targetIntensity, pingPongValue / halfDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }   

        _light.intensity = 0;
        _effectCoroutine = null;
    }
}
