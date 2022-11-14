using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct GraveyardAspect : IAspect
{
    public readonly Entity Entity;

    private readonly TransformAspect _transfromAspect;

    private readonly RefRO<GraveyardProperties> _graveyardProperties;
    private readonly RefRW<GraveyardRandom> _graveyardRandom;
    

    public int NumberTombstoneToSpawn => _graveyardProperties.ValueRO.NumberToSpawn;
    public Entity TombstonePrefab => _graveyardProperties.ValueRO.TombstonePrefab;

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
            randomPosition = _graveyardRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorne
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
