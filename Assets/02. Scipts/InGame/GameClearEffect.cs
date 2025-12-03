using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameClearEffect : MonoBehaviour
{
    [Header("이펙트 수치 설정")]
    [SerializeField] private float _duration;
    [SerializeField] private float _targetGlobalIntensity;
    [SerializeField] private float _targetPlayerIntensity;
    [SerializeField] private float _targetOuterRadius;

    [Header("조명 바인딩")]
    [SerializeField] private Light2D _globalLight;
    [SerializeField] private Light2D _playerLight;

    private float _startGlobalIntensity;
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
        _startGlobalIntensity = _globalLight.intensity;
        _startPlayerIntensity = _playerLight.intensity;
        _startOuterRadius = _playerLight.pointLightOuterRadius;

        StartCoroutine(GameClearLightEffect());
    }

    private IEnumerator GameClearLightEffect()
    {
        float timer = 0f;

        float inverseDuration = 1f / _duration;

        float deltaGlobalIntensity = _targetGlobalIntensity - _startGlobalIntensity;
        float deltaPlayerIntensity = _targetPlayerIntensity - _startPlayerIntensity;
        float deltaOuterRadius = _targetOuterRadius - _startOuterRadius;

        while (timer < _duration)
        {
            timer += Time.deltaTime;

            float timeRate = timer * inverseDuration;

            if (timeRate > 1f) timeRate = 1f;

            _globalLight.intensity = _startGlobalIntensity + (deltaGlobalIntensity * timeRate);
            _playerLight.intensity = _startPlayerIntensity + (deltaPlayerIntensity * timeRate);
            _playerLight.pointLightOuterRadius = _startOuterRadius + (deltaOuterRadius * timeRate);

            yield return null;
        }
        UIManager.Instance.OpenUI<GameClearUI>(new BaseUIData());
    }
}
