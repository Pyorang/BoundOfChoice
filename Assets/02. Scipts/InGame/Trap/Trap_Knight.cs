using UnityEngine;

public class Trap_Knight : MonoBehaviour
{
    public bool _activated = false;

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
        CameraController.Instance.StartShake();

        Vector2 attackCenter = (Vector2)transform.position + (Vector2)transform.right * _boxOffset.x + (Vector2)transform.up * _boxOffset.y;

        float finalAngle = transform.eulerAngles.z + _boxAngle;

        Collider2D[] hits = Physics2D.OverlapBoxAll(
            attackCenter,
            _boxSize,
            finalAngle,
            _targetLayer
        );

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerHealth.Instance.TakeDamage(_damage);
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!_activated) return;
            if (!CanAttack()) return;

            _animator.SetTrigger(AttackTrigger);
            SaveLastAttackTime();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 attackCenter = transform.position + transform.right * _boxOffset.x + transform.up * _boxOffset.y;

        Gizmos.color = Color.red;

        Quaternion finalRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + _boxAngle);

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(
            attackCenter,
            finalRotation,
            Vector3.one
        );

        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(_boxSize.x, _boxSize.y, 0.01f));

        Gizmos.matrix = Matrix4x4.identity;
    }
}
