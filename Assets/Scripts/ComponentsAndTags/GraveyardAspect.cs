using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct GraveyardAspect : IAspect
{
    public readonly Entity Entity;

    private readonly TransformAspect _transfromAspect;

    private readonly RefRO<GraveyardProperties> _graveyardProperties;
    private readonly RefRW<GraveyardRandom> _graveyardRandom;
    private readonly RefRW<ZombieSpawnPoints> _zombieSpawnPoints;
    private readonly RefRW<ZombieSpawnTimer> _zombieSpawnTimer;
    

    public int NumberTombstoneToSpawn => _graveyardProperties.ValueRO.NumberToSpawn;
    public Entity TombstonePrefab => _graveyardProperties.ValueRO.TombstonePrefab;

    public NativeArray<float3> ZombieSpawnPoint
    {
        get => _zombieSpawnPoints.ValueRO.Value;
        set => _zombieSpawnPoints.ValueRW.Value = value;
    }

    public float ZombieSpawnTimer
    {
        get => _zombieSpawnTimer.ValueRO.Value;
        set => _zombieSpawnTimer.ValueRW.Value = value;
    }
    public bool TimeToSpawn => ZombieSpawnTimer <= 0;
    public UniformScaleTransform GetRandomTombstoneTransform()
    {
        return new()
        {
            Position = GetRandomPosition(),
            Rotation = GetRandomQuaternion(),
            Scale = GetRandomScale(0.2f)
        };
    }

    private float3 GetRandomPosition()
    {
        float3 randomPosition;
        do
        {
            randomPosition = _graveyardRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
        } while (math.distancesq(_transfromAspect.Position, randomPosition) <= BRAIN_SAFETY_RADIUS_SQ);
 
        return randomPosition;
    }
    private float3 MinCorner => _transfromAspect.Position - HalfDimension;
    private float3 MaxCorner => _transfromAspect.Position + HalfDimension;
    private float3 HalfDimension => new() { 
        x = _graveyardProperties.ValueRO.FieldDimensions.x * 0.5f,
        y = 0,
        z = _graveyardProperties.ValueRO.FieldDimensions.y * 0.5f
    };

    private const float BRAIN_SAFETY_RADIUS_SQ = 100f;
    private quaternion GetRandomQuaternion() => quaternion.RotateY(_graveyardRandom.ValueRW.Value.NextFloat(-0.5f, 0.5f));
    private float GetRandomScale(float min) => _graveyardRandom.ValueRW.Value.NextFloat(min, 1);

}
