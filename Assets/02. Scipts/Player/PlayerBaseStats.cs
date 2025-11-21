using UnityEngine;

[CreateAssetMenu(menuName = "Stats/PlayerBaseStats")]
public class PlayerBaseStats : ScriptableObject
{
    public int MaxHealth;
    public int MaxMana;
    public float MinSpeed;
    public float MaxSpeed;
    public float AttackPower;
    public float JumpHeight;
}
