using System.Collections;
using Unity.Entities;
using UnityEngine;


public class ZombieMono : MonoBehaviour
{
    public float ZombieRiseRate;
}

public class Baker : Baker<ZombieMono>
{
    public override void Bake(ZombieMono authoring)
    {
        AddComponent(new ZombieRiseRate { Value = authoring.ZombieRiseRate});
    }
}
