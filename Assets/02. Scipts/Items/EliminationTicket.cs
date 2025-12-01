using UnityEngine;

public class EliminationTicket : ItemBase
{
    public override bool ApplyEffect()
    {
        PoolManager.Instance.ReleaseAllObjects(EPoolType.SkeletonSwrodsman);
        PoolManager.Instance.ReleaseAllObjects(EPoolType.SkeletonArbalist);
        PoolManager.Instance.ReleaseAllObjects(EPoolType.SkeletonElite);
        PoolManager.Instance.ReleaseAllObjects(EPoolType.SkeletonNecro);
        PoolManager.Instance.ReleaseAllObjects(EPoolType.BringerOfDeath);
        return true;
    }
}
