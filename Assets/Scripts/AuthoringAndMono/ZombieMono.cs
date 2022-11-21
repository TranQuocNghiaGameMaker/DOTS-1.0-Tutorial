using System.Collections;
using Unity.Entities;
using UnityEngine;


public class ZombieMono : MonoBehaviour
{
    public float ZombieRiseRate;
    public float WalkSpeed;
    public float WalkAmplitude;
    public float WalkFrequency;
    public float EatDamagePerSecond;
    public float EatAmplitude;
    public float EatFrequency;
}

public class ZombieBaker : Baker<ZombieMono>
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
        AddComponent(new ZombieEatProperty
        {
            EatDamagePerSecond = authoring.EatDamagePerSecond,
            EatAmplitude = authoring.EatAmplitude,
            EatFrequency = authoring.EatFrequency
        });
        AddComponent<ZombieTimer>();
        AddComponent<ZombieHeading>();
        AddComponent<ZombieTag>();
    }
}
