﻿using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct ZombieSpawnPoints : IComponentData
{
    public NativeArray<float3> Value;
}
