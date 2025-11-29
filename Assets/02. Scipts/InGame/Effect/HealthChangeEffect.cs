using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthChangeEffect : MonoBehaviour
{
    [Header("연출 설정")]
    [Space]
    [SerializeField] private float _totalDuration = 0.5f;

    [Header("이펙트 색깔 조정")]
    [Space]
    [SerializeField] private Color _healColor;
    [SerializeField] private Color _hurtColor;

    [Header("이미지 컴포넌트")]
    [Space]
    [SerializeField] private Image _image;

    private Coroutine _healthChangeEffect;

    private void Awake()
    {
        PlayerHealth.OnHealthChange += ProcessEffect;
    }

    private void OnDestroy()
    {
        PlayerHealth.OnHealthChange -= ProcessEffect;
    }

    private void ProcessEffect(bool isHealing)
    {
        if (_healthChangeEffect != null)
        {
            StopCoroutine(_healthChangeEffect);
        }

        _healthChangeEffect = StartCoroutine(StartHurtEffect(isHealing));
    }

    private IEnumerator StartHurtEffect(bool isHealing)
    {
        PlaySound(isHealing);
        Color startImageColor = isHealing ? _healColor : _hurtColor;
        _image.color = startImageColor;
        float elapsedTime = 0f;

        while (elapsedTime < _totalDuration)
        {
            elapsedTime += Time.deltaTime;

            float ratio = (elapsedTime / _totalDuration);

            Color newColor = _image.color;
            newColor.a = startImageColor.a * (1.0f - ratio);
            _image.color = newColor;

            yield return null;
        }

        _healthChangeEffect = null;
        yield return null;
    }

    private void PlaySound(bool isHealing)
    {
        if(isHealing)
        {
            AudioManager.Instance.Play(AudioType.SFX, "PlayerHeal");
        }
        else
        {
            AudioManager.Instance.Play(AudioType.SFX, "PlayerHurt");
        }
    }
}
