using Unity.Entities;

public struct ZombieWalkProperty : IComponentData,IEnableableComponent
{
    public float WalkingSpeed;
    public float WalkAmplitude;
    public float WalkFrequency;
}

public struct ZombieHeading : IComponentData
{
    public float Value;
}

public struct ZombieTimer : IComponentData {
    public float Value;
}

public struct ZombieTag : IComponentData { }
