using System.Collections;
using UnityEngine;

public class SpiritEffect : MonoBehaviour
{
    [Header("파티클 프리팹")]
    [SerializeField] private GameObject _trailEffectPrefab;
    [SerializeField] private GameObject _gainEffectPrefab;
    [SerializeField] private float _effectDuration = 1.0f;
    [SerializeField] private AnimationCurve _speedCurve;

    [Header("위치 설정")]
    [SerializeField] private Transform _startTransform;
    [SerializeField] private Transform _endTransform;
    [SerializeField] private Vector3 _controlPoint1;
    [SerializeField] private Vector3 _controlPoint2;
    private Vector3 _worldEndPosition;
    private const float DestroyTime = 3f;

    private void Awake()
    {
        SpiritManager.OnSpiritGained += StartEffect;
        _worldEndPosition = Camera.main.ScreenToWorldPoint(_endTransform.position);
    }

    private void OnDestroy()
    {
        SpiritManager.OnSpiritGained -= StartEffect;
    }

    public void StartEffect()
    {
        GameObject effectInstance = Instantiate(_trailEffectPrefab, _startTransform.position, Quaternion.identity);
        StartCoroutine(MoveBezierCurveEffect(effectInstance, _startTransform.position, _worldEndPosition));
    }

    public void GainEffect()
    {
        SpiritManager.Instance.RefreshSpiritUI();
        GameObject effectInstance = Instantiate(_gainEffectPrefab, _worldEndPosition, Quaternion.identity);
        Destroy(effectInstance, DestroyTime);
    }

    private IEnumerator MoveBezierCurveEffect(GameObject effect, Vector3 startPosition, Vector3 endPosition)
    {
        float timer = 0f;

        while (timer < _effectDuration)
        {
            timer += Time.deltaTime;
            float t = timer / _effectDuration;

            float curvedT = _speedCurve.Evaluate(t);

            Vector3 currentPosition = GetBezierPoint(startPosition, _controlPoint1, _controlPoint2, endPosition, curvedT);
            effect.transform.position = currentPosition;

            yield return null;
        }

        Destroy(effect);
        GainEffect();
    }

    public static Vector3 GetBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0; // (1-t)^3 * P0
        p += 3 * uu * t * p1; // 3 * (1-t)^2 * t * P1
        p += 3 * u * tt * p2; // 3 * (1-t) * t^2 * P2
        p += ttt * p3;        // t^3 * P3

        return p;
    }
}
