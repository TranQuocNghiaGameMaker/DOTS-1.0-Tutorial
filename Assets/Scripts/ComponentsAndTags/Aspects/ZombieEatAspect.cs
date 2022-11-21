using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

//Aspect groups Data Component so it can be easily accessed and modified 
public readonly partial struct ZombieEatAspect : IAspect
{
    public readonly Entity Entity;

    private readonly TransformAspect _transfromAspect;

    private readonly RefRW<ZombieTimer> _zombieTimer;
    private readonly RefRO<ZombieEatProperty> _zombieWEatProperty;
    private readonly RefRO<ZombieHeading> _zombieHeading;

    private float _eatDamagePerSecond => _zombieWEatProperty.ValueRO.EatDamagePerSecond;
    private float _eatAmplitude => _zombieWEatProperty.ValueRO.EatAmplitude;
    private float _eatFrequency => _zombieWEatProperty.ValueRO.EatFrequency;
    private float _heading => _zombieHeading.ValueRO.Value;

    private float _eatTimer
    {
        get => _zombieTimer.ValueRO.Value;
        set => _zombieTimer.ValueRW.Value = value;
    }

    public void Eat(float deltaTime)
    {
        _eatTimer += deltaTime;
        var eatAngle = _eatAmplitude * math.sin(_eatFrequency * _eatTimer);
        _transfromAspect.Rotation = quaternion.Euler(eatAngle,_heading,0);
    }
    

    
}
