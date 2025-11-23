using System.Collections;
using UnityEngine;

public class CameraController : SingletonBehaviour<CameraController>
{
    private bool _isShakerOn = true;
    private Camera _camera;

    private UserSettingsData _userSettingsData;

    private Vector3 _startPosition;
    private Coroutine _shakeCoroutine;

    protected override void Init()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        Init();
        SyncUserSettings();
    }

    public void SetShakerOn(bool isOn)
    {
        _isShakerOn = isOn;
    }

    public void StartShake(float duration, float power)
    {
        if(_isShakerOn)
        {
            if (_shakeCoroutine != null)
            {
                StopCoroutine(_shakeCoroutine);
                if (_camera != null)
                {
                    _camera.transform.localPosition = _startPosition;
                }
            }

            if (_camera == null) return;

            _startPosition = _camera.transform.localPosition;
            _shakeCoroutine = StartCoroutine(ShakeCoroutine(duration, power));
        }
    }

    public void StopShake()
    {
        if (_shakeCoroutine != null)
        {
            StopCoroutine(_shakeCoroutine);
            _shakeCoroutine = null;
        }

        if (_camera != null)
        {
            _camera.transform.localPosition = _startPosition;
        }
    }

    private IEnumerator ShakeCoroutine(float duration, float power)
    {
        float elapsedTime = 0f;

        Vector3 originalPosition = _startPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            Vector3 randomOffset = new Vector3(
                Random.Range(-1f, 1f) * power,
                Random.Range(-1f, 1f) * power,
                0f
            );

            _camera.transform.localPosition = originalPosition + randomOffset;
            yield return null;
        }

        StopShake();
    }

    public void SyncUserSettings()
    {
        _userSettingsData = UserDataManager.Instance.GetUserData<UserSettingsData>();
        _isShakerOn = _userSettingsData.IsVibrationOn;
    }
}