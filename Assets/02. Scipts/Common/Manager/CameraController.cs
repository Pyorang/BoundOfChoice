using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : SingletonBehaviour<CameraController>
{
    private bool _isCameraEffectOn = true;
    private Camera _camera;

    private Vector3 _startPosition;
    private Coroutine _shakeCoroutine;
    private Coroutine _zoomCoroutine;

    [Header("연출 설정")]
    [SerializeField] private float _shakeDuration = 0.5f;
    [SerializeField] private float _shakePower = 0.2f;

    protected override void Init()
    {
        base.Init();
        SceneManager.sceneLoaded += OnSceneLoaded;
        _camera = Camera.main;
        _startPosition = _camera.transform.localPosition;
    }

    private void Start()
    {
        SyncUserSettings();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _camera = Camera.main;
        _startPosition = _camera.transform.localPosition;
    }

    public void SetCameraEffectOn(bool isOn)
    {
        _isCameraEffectOn = isOn;
    }

    public void StartShake()
    {
        if (!_isCameraEffectOn || _camera == null) return;

        StopShake();

        _shakeCoroutine = StartCoroutine(ShakeCoroutine(_shakeDuration, _shakePower));
    }

    public void StartShake(float duration, float power)
    {
        if (!_isCameraEffectOn || _camera == null) return;

        StopShake();

        _shakeCoroutine = StartCoroutine(ShakeCoroutine(duration, power));
    }

    public void StopShake()
    {
        if (_shakeCoroutine == null) return;
   
        StopCoroutine(_shakeCoroutine);
        _shakeCoroutine = null;

        if (_camera == null) return;
        
        _camera.transform.localPosition = _startPosition;
    }

    private IEnumerator ShakeCoroutine(float duration, float power)
    {
        float elapsedTime = 0f;

        Vector3 startPosition = _startPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            Vector3 randomOffset = Random.insideUnitCircle.normalized * power;

            _camera.transform.localPosition = startPosition + randomOffset;
            yield return null;
        }

        StopShake();
    }
    public void StartZoom(float duration, float size, Vector2 target)
    {
        if (!_isCameraEffectOn || _camera == null) return;

        if (_zoomCoroutine != null)
        {
            StopCoroutine(_zoomCoroutine);
        }

        _zoomCoroutine = StartCoroutine(ZoomCoroutine(duration, size, target));
    }

    private IEnumerator ZoomCoroutine(float duration, float size, Vector2 target)
    {
        float elapsedTime = 0f;
        float startSize = _camera.orthographicSize;

        Vector3 startPosition = _startPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            _camera.orthographicSize = Mathf.Lerp(startSize, size, t);

            _camera.transform.position = Vector3.Lerp(startPosition, target, t);

            yield return null;
        }

        _camera.orthographicSize = startSize;
        _camera.transform.position = startPosition;

        _zoomCoroutine = null;
    }

    public void SyncUserSettings()
    {
        var _userSettingsData = UserDataManager.Instance.GetUserData<UserSettingsData>();
        _isCameraEffectOn = _userSettingsData.IsVibrationOn;
    }
}