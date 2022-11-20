using System.Collections;
using Unity.Entities;
using UnityEngine;


public class BrainMono : MonoBehaviour
{

}

public class BrainBaker : Baker<BrainMono>
{
    public override void Bake(BrainMono authoring)
    {
        AddComponent<BrainTag>();
    }
}