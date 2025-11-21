using UnityEngine;

[CreateAssetMenu(menuName = "Stats/PlayerBaseStats")]
public class PlayerBaseStats : ScriptableObject
{
    public float MaxHealth;
    public float MaxMana;
    public float Speed;
    public float AttackPower;
    public float JumpHeight;
}
