using UnityEngine;

public class PlayerTestInput : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private PlayerMana _playerMana;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerMana = GetComponent<PlayerMana>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _playerHealth.TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playerHealth.Heal(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _playerMana.TryUseMana(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _playerMana.RegenerateMana(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _playerMovement.MoveSpeedUp(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _playerMovement.MoveSpeedDown(1);
        }
    }
}
