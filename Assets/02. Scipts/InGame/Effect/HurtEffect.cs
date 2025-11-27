using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HurtEffect : MonoBehaviour
{
    [Header("연출 설정")]
    [Space]
    [SerializeField] private float _totalDuration = 0.5f;

    [SerializeField] private Image _image;
    private Color _imageColor;

    private Coroutine _hurtEffect;

    private void Awake()
    {
        _imageColor = _image.color;
    }

    private void OnEnable()
    {
        if(_hurtEffect != null)
        {
            StopCoroutine(_hurtEffect);
        }

        _hurtEffect = StartCoroutine(StartHurtEffect());
    }

    public void OnDisable()
    {
        _image.color = _imageColor;
    }

    private IEnumerator StartHurtEffect()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _totalDuration)
        {
            elapsedTime += Time.deltaTime;

            float ratio = (elapsedTime / _totalDuration);

            Color newColor = _image.color;
            newColor.a = _imageColor.a * (1.0f - ratio);
            _image.color = newColor;

            yield return null;
        }

        _hurtEffect = null;
        gameObject.SetActive(false);
        yield return null;
    }
}
