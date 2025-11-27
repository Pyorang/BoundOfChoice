using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameClearEffect : MonoBehaviour
{
    [Header("이펙트 수치 설정")]
    [SerializeField] private float _duration;
    [SerializeField] private float _targetGobalIntensity;
    [SerializeField] private float _targetPlayerIntensity;
    [SerializeField] private float _targetOuterRadius;

    [Header("조명 바인딩")]
    [SerializeField] private Light2D _globalLight;
    [SerializeField] private Light2D _playerLight;

    private float _startGobalIntensity;
    private float _startPlayerIntensity;
    private float _startOuterRadius;

    private void Awake()
    {
        SpiritManager.OnSpiritCompleted += StartEffect;
    }

    private void OnDestroy()
    {
        SpiritManager.OnSpiritCompleted -= StartEffect;
    }

    private void StartEffect()
    {
        _startGobalIntensity = _globalLight.intensity;
        _startPlayerIntensity = _playerLight.intensity;
        _startOuterRadius = _playerLight.pointLightOuterRadius;

        StartCoroutine(GameClearLightEffect());
    }

    private IEnumerator GameClearLightEffect()
    {
        float timer = 0f;

        float invDuration = 1f / _duration;

        float deltaGlobalIntensity = _targetGobalIntensity - _startGobalIntensity;
        float deltaPlayerIntensity = _targetPlayerIntensity - _startPlayerIntensity;
        float deltaOuterRadius = _targetOuterRadius - _startOuterRadius;

        while (timer < _duration)
        {
            timer += Time.deltaTime;

            float t = timer * invDuration;

            if (t > 1f) t = 1f;

            _globalLight.intensity = _startGobalIntensity + (deltaGlobalIntensity * t);
            _playerLight.intensity = _startPlayerIntensity + (deltaPlayerIntensity * t);
            _playerLight.pointLightOuterRadius = _startOuterRadius + (deltaOuterRadius * t);

            yield return null;
        }
        UIManager.Instance.OpenUI<GameClearUI>(new BaseUIData());
    }
}
