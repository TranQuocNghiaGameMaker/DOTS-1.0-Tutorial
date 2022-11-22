using System.Collections;
using Unity.Entities;
using UnityEngine;


public struct BrainTag : IComponentData
{

}

public struct BrainHealth : IComponentData {
    public float Value;
    public float Max;
}


