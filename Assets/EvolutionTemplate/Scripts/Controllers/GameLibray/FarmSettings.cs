using System;
using UnityEngine;

[Serializable]
public struct FarmSettings {
    [SerializeField]
    public Rect SpawnArea;

    [SerializeField]
    public float ElementSpawnTime;

    [SerializeField]
    public float MaxAmountOfAwaitingCrates;
}