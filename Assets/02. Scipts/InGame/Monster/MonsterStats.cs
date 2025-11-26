using System.Collections;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    [Header("몬스터 데이터")]
    [SerializeField] private MonsterData _baseData;

    private int _currentHealth;
    private int _currentMaxHealth;
    private int _currentAttackPower;
    private float _currentMoveSpeed;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _currentMaxHealth;
    public int AttackPower => _currentAttackPower;
    public float MoveSpeed => _currentMoveSpeed;

    private Coroutine _dotDamageCoroutine;
    private Coroutine _bindCoroutine;

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        if (_baseData == null)
        {
            Debug.LogError("MonsterData is missing!");
            return;
        }

        _currentMaxHealth = _baseData.MaxHealth;
        _currentHealth = _currentMaxHealth;
        _currentAttackPower = _baseData.AttackPower;
        _currentMoveSpeed = _baseData.MoveSpeed;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0) return;
        // Note : 피격 애니메이션 실행
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Death();
        }
    }
    
    public void TakeDotDamage(int damage, float duration, float interval)
    {
        if (_currentHealth <= 0) return;
        if (_dotDamageCoroutine != null)
        {
            StopCoroutine(_dotDamageCoroutine);
        }
        _dotDamageCoroutine = StartCoroutine(ProcessDotDamage(damage, duration, interval));
    }

    public void TakeBind(float duration)
    {
        if (_bindCoroutine != null)
        {
            StopCoroutine(_bindCoroutine);
        }
        _bindCoroutine = StartCoroutine(ProcessBind(duration));
    }

    private IEnumerator ProcessBind(float duration)
    {
        // Note : 애니메이터 멈추기
        _currentMoveSpeed = 0;
        yield return new WaitForSeconds(duration);
        // Note : 애니메이터 다시 재생
        _currentMoveSpeed = _baseData.MoveSpeed;
        _bindCoroutine = null;
    }

    private IEnumerator ProcessDotDamage(int damage, float duration, float interval)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            yield return new WaitForSeconds(interval);
            TakeDamage(damage);
            elapsedTime += interval;
        }
        _dotDamageCoroutine = null;
    }

    private void Death()
    {
        // Note : 사망 애니메이션 실행
        StopAllCoroutines();

        // Note : 사망 애니메이션 이후 Pool에 반납하게 수정 필요
        gameObject.SetActive(false);
    }
}