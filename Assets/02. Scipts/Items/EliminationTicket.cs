using UnityEngine;

public class EliminationTicket : ItemBase
{
    private const int MaxDamage = 9999;

    public override bool ApplyEffect()
    {
        // NOTE : 오브젝트 풀링 구현 후 풀링 인스턴스에 접근하여 RemoveAllMonsters를 호출하도록 수정한다.
        MonsterController[] monsters = FindObjectsByType<MonsterController>(FindObjectsSortMode.None);
        foreach(MonsterController monster in monsters)
        {
            monster.TakeDamage(MaxDamage);
        }
        return true;
    }
}
