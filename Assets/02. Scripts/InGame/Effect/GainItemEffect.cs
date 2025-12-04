using UnityEngine;
using System.Collections;

public class GainItemEffect : MonoBehaviour
{
    [Header("획득 연출 설정")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _attractSpeed = 5f; 
    [SerializeField] private EPoolType _itemPoolType;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Transform _playerTransform;
    private bool _isCollecting = false;

    private static readonly string s_itemSound = "Item";

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _playerTransform = PlayerMovement.Instance.transform;
    }

    private void OnEnable()
    {
        _isCollecting = false;

        Color color = _spriteRenderer.color;
        color.a = 1f;
        _spriteRenderer.color = color;
    }

    public bool PlayGainEffect()
    {
        if (_isCollecting) return false;
        _isCollecting = true;

        AudioManager.Instance.Play(AudioType.SFX, s_itemSound);
        StartCoroutine(GainEffectRoutine());

        return true;
    }

    private void TrackPlayerX()
    {
        float newX = Mathf.Lerp(transform.position.x, _playerTransform.position.x, Time.deltaTime * _attractSpeed);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private IEnumerator GainEffectRoutine()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

        yield return null;

        while (_rigidbody.linearVelocityY > 0)
        {
            TrackPlayerX();
            yield return null;
        }

        float timer = 0f;
        Color startColor = _spriteRenderer.color;
        Color newColor = startColor;

        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / _fadeDuration);

            newColor.a = alpha;
            _spriteRenderer.color = newColor;

            TrackPlayerX();

            yield return null;
        }

        PoolManager.Instance.ReleaseObject(_itemPoolType, gameObject);
    }
}
