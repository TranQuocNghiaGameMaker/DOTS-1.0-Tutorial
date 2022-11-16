using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

//Aspect groups Data Component so it can be easily accessed and modified 
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

    public Entity ZombiePrefab => _graveyardProperties.ValueRO.ZombiePrefab;

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

    public float ZombieSpawnRate => _graveyardProperties.ValueRO.ZombieSpawnRate;
    public UniformScaleTransform GetRandomTombstoneTransform()
    {
        return new()
        {
            Position = GetRandomPosition(),
            Rotation = GetRandomQuaternion(),
            Scale = GetRandomScale(0.2f)
        };
    }
    public UniformScaleTransform GetZombieSpawnPoint()
    {
        var position = GetRandomZombieSpawnPoint;
        return new UniformScaleTransform
        {
            Position = position,
            Rotation = quaternion.RotateY(HelperFunction.GetHeading(position, _transfromAspect.Position)),
            Scale = 1f
        };
    }
    /// <summary>
    /// Get a random position in the ZombieSpawnPoint list
    /// </summary>
    private float3 GetRandomZombieSpawnPoint
    {
        get => ZombieSpawnPoint[_graveyardRandom.ValueRW.Value.NextInt(ZombieSpawnPoint.Length)];
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
