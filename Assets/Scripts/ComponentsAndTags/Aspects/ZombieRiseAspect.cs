using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

//Aspect groups Data Component so it can be easily accessed and modified 
public readonly partial struct ZombieRiseAspect : IAspect
{
    public readonly Entity Entity;

    private readonly TransformAspect _transfromAspect;

    private readonly RefRO<ZombieRiseRate> _zombieRiseRate;

    public void Rise(float deltaTime)
    {
        _transfromAspect.Position += math.up() * _zombieRiseRate.ValueRO.Value * deltaTime;
    }

    public bool IsAboveGround => _transfromAspect.Position.y >= 0;

    public void SetGroundLevel()
    {
        var position = _transfromAspect.Position;
        position.y = 0;
        _transfromAspect.Position = position;
    }
}
