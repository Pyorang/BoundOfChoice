using System;
using UnityEngine;

public class Blood : MonoBehaviour
{
    [Header("피 종류 선택 (1~3)")]
    [Space]
    [SerializeField] private int _bloodType;

    private static readonly int _maxBloodNumber = 3;

    private void DisableObject()
    {
        string PoolingObjectName = $"Blood{_bloodType}";
        EPoolType poolingObject = (EPoolType)Enum.Parse(typeof(EPoolType), PoolingObjectName, true);

        PoolManager.Instance.ReleaseObject(EPoolType.Blood1, gameObject);
    }
}
