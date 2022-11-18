using System.Collections;
using Unity.Entities;
using UnityEngine;


public class ZombieMono : MonoBehaviour
{
    public float ZombieRiseRate;
    public float WalkSpeed;
    public float WalkAmplitude;
    public float WalkFrequency;
}

public class Baker : Baker<ZombieMono>
{
    public override void Bake(ZombieMono authoring)
    {
        AddComponent(new ZombieRiseRate { Value = authoring.ZombieRiseRate});
        AddComponent(new ZombieWalkingProperty
        {
            WalkingSpeed = authoring.WalkSpeed,
            WalkAmplitude = authoring.WalkAmplitude,
            WalkFrequency = authoring.WalkFrequency
        });
        AddComponent<ZombieTimer>();
        AddComponent<ZombieHeading>();
        AddComponent<ZombieTag>();
    }
}
