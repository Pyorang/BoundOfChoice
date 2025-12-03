using UnityEngine;

public class Trap_Knight : MonoBehaviour
{
    public bool _activated = false;

    [Header("방향 반대 여부")]
    [Space]
    [SerializeField] private bool _isLeft = false;

    [Header("데미지")]
    [Space]
    [SerializeField] private int _damage = 20;

    [Header("사정거리")]
    [Space]
    [SerializeField] private Vector2 _boxSize = new Vector2(4.08f, 4.76f);
    [SerializeField] private Vector2 _boxOffset = new Vector2(1.3f, 0.26f);
    [SerializeField] private float _boxAngle = 0f;

    [Header("재활성화 시간")]
    [Space]
    [SerializeField] private float _attackCoolTime = 5f;
    private float _lastAttackTime;

    [Header("레이어 마스크 체크")]
    [Space]
    [SerializeField] private LayerMask _targetLayer = 3;

    private Animator _animator;
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ActivateTrap() => _activated = true;
    public void DeactivateTrap() => _activated = false;
    public bool CanAttack() => Time.time - _lastAttackTime >= _attackCoolTime;
    public void SaveLastAttackTime() => _lastAttackTime = Time.time;

    public void Attack()
    {
        AudioManager.Instance.Play(AudioType.SFX, "TrapAttack");
        CameraController.Instance.StartShake();

        // 2. 공격 중심 위치 및 방향 계산 (Attack과 Gizmo에서 동일하게 사용)
        // moveDirection은 transform.right를 기준으로 _isLeft에 따라 방향이 결정됩니다.
        Vector2 direction = _isLeft ? -(Vector2)transform.right : (Vector2)transform.right;

        // 공격 박스의 중심 위치 계산 (오브젝트 회전 + 방향 + 오프셋 고려)
        Vector2 attackCenter = (Vector2)transform.position
                             + direction * _boxOffset.x
                             + (Vector2)transform.up * _boxOffset.y;

        // 3. 최종 회전 각도 계산
        float finalAngle = transform.eulerAngles.z + _boxAngle;

        // 4. OverlapBoxAll 실행
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            attackCenter,
            _boxSize,
            finalAngle,
            _targetLayer
        );

        // 5. 감지된 물체 처리
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerHealth.Instance.TakeDamage(_damage);
                break;
            }
        }
    }

    private void PlayTrapActivatedSound()
    {
        AudioManager.Instance.Play(AudioType.SFX, "TrapActivated");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!_activated) return;
            if (!CanAttack()) return;

            _animator.SetTrigger(AttackTrigger);
            AudioManager.Instance.Play(AudioType.SFX, "TrapReady");
            SaveLastAttackTime();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 1. 공격 중심 위치 계산 (Attack 함수와 동일)
        Vector3 direction = _isLeft ? -(Vector3)transform.right : (Vector3)transform.right;
        Vector3 attackCenter = transform.position + direction * _boxOffset.x + transform.up * _boxOffset.y;

        // 2. Gizmo 시각화 설정
        Gizmos.color = Color.red;

        // 3. 회전 행렬을 사용하여 위치, 회전, 크기 변환 적용
        Quaternion finalRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + _boxAngle);

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(
            attackCenter,
            finalRotation,
            Vector3.one
        );

        Gizmos.matrix = rotationMatrix;

        // 4. 박스 그리기
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(_boxSize.x, _boxSize.y, 0.01f));

        // 5. Gizmos.matrix 초기화 (필수)
        Gizmos.matrix = Matrix4x4.identity;
    }
}
