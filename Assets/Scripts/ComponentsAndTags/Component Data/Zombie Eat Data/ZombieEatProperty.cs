using Unity.Entities;

public struct ZombieEatProperty : IComponentData,IEnableableComponent
{
    public float EatDamagePerSecond;
    public float EatAmplitude;
    public float EatFrequency;
}

