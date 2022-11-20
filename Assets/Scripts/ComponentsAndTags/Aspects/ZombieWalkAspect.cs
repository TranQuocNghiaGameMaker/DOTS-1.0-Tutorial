using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

//Aspect groups Data Component so it can be easily accessed and modified 
public readonly partial struct ZombieWalkAspect : IAspect
{
    public readonly Entity Entity;

    private readonly TransformAspect _transfromAspect;

    private readonly RefRW<ZombieTimer> _zombieTimer;
    private readonly RefRO<ZombieWalkingProperty> _zombieWalkingProperty;
    private readonly RefRO<ZombieHeading> _zombieHeading;

    private float _walkSpeed => _zombieWalkingProperty.ValueRO.WalkingSpeed;
    private float _walkAmplitude => _zombieWalkingProperty.ValueRO.WalkAmplitude;
    private float _walkFrequency => _zombieWalkingProperty.ValueRO.WalkFrequency;
    private float _heading => _zombieHeading.ValueRO.Value;

    private float _walkTimer
    {
        get => _zombieTimer.ValueRO.Value;
        set => _zombieTimer.ValueRW.Value = value;
    }

    public void Walk(float deltaTime)
    {
        _walkTimer += deltaTime;
        _transfromAspect.Position += _transfromAspect.Forward * _walkSpeed * deltaTime;
        var swayAngle = _walkAmplitude * math.sin(_walkFrequency * _walkTimer);
        _transfromAspect.Rotation = quaternion.Euler(0, _heading, swayAngle);
    }

    public bool IsInStopRange(float3 brainPos, float brainRadius)
    {
        return math.distancesq(brainPos, _transfromAspect.Position) <= brainRadius;
    }
}
