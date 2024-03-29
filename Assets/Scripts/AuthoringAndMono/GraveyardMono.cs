﻿using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class GraveyardMono : MonoBehaviour
{
    public float2 FieldDimensions;
    public int NumberToSpawn;
    public GameObject TombstonePrefab;
    public uint RandomSeed;
    public GameObject ZombiePrefab;
    public float ZombieSpawnRate;
}

public class GraveyardBaker : Baker<GraveyardMono>
{
    public override void Bake(GraveyardMono authoring)
    {
        AddComponent(new GraveyardProperties
        {
            FieldDimensions = authoring.FieldDimensions,
            NumberToSpawn = authoring.NumberToSpawn,
            TombstonePrefab = GetEntity(authoring.TombstonePrefab),
            ZombiePrefab = GetEntity(authoring.ZombiePrefab),
            ZombieSpawnRate = authoring.ZombieSpawnRate
        });
        AddComponent(new GraveyardRandom
        {
            Value = Random.CreateFromIndex(authoring.RandomSeed)
        });
        AddComponent<ZombieSpawnPoints>();
        AddComponent<ZombieSpawnTimer>();
    }
}
