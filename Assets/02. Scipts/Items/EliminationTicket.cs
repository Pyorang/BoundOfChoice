using UnityEngine;

public class EliminationTicket : ItemBase
{
    private const int MaxDamage = 9999;

    public override void ApplyEffect()
    {
        // NOTE : 오브젝트 풀링 구현 후 풀링 인스턴스에 접근하여 RemoveAllMonsters를 호출하도록 수정한다.
        MonsterStats[] monsters = FindObjectsByType<MonsterStats>(FindObjectsSortMode.None);
        foreach(MonsterStats monster in monsters)
        {
            monster.TakeDamage(MaxDamage);
        }
    }
}
