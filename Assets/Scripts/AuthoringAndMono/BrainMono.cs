using System.Collections;
using Unity.Entities;
using UnityEngine;


public class BrainMono : MonoBehaviour
{
    public float CurrentHealth;
    public float MaxHealth;
}

public class BrainBaker : Baker<BrainMono>
{
    public override void Bake(BrainMono authoring)
    {
        AddComponent<BrainTag>();
        AddComponent(new BrainHealth
        {
            Value = authoring.CurrentHealth,
            Max = authoring.MaxHealth
        });
        AddBuffer<BrainDamageBufferElement>();
    }
}