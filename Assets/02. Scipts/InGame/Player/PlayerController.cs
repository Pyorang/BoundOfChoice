using System;
using UnityEngine;

public enum EPlayerState
{
    Idle,
    Move,
    Attack,
    Hit,
    Die
}

public class PlayerController : MonoBehaviour
{
    public static event Action<int> OnStateChanged;

    private PlayerMovement _movement;
    private PlayerCombat _combat;
    private PlayerInteraction _interaction;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _combat = GetComponent<PlayerCombat>();
        _interaction = GetComponent<PlayerInteraction>();
    }

    private void Update()
    {
        GetKeyInput();
        GetKeyMoveInput();
        GetAttackKeyInput();
    }

    private void GetKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _interaction.DoInteraction();
        }
    }

    private void GetKeyMoveInput()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        if (xMovement != 0)
        {
            _movement.SetDirection(Mathf.RoundToInt(xMovement));
        }
        _movement.SetMovement(xMovement);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _movement.Jump();
        }
    }

    private void GetAttackKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _combat.OnAttack();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _combat.ChangeCharacter();
        }

        // Note : 임시 키 설정 값
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _combat.UseFireBall();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _combat.UseIceAge();
        }
    }
}
