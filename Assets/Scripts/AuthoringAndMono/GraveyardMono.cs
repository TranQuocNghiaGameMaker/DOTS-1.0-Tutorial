using System.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class GraveyardMono : MonoBehaviour
{
    public float2 FieldDimensions;
    public int NumberToSpawn;
    public GameObject TombstonePrefab;
}

public class GraveyardBaker : Baker<GraveyardMono>
{
    public override void Bake(GraveyardMono authoring)
    {
        AddComponent(new GraveyardProperty
        {
            FieldDimensions = authoring.FieldDimensions,
            NumberToSpawn = authoring.NumberToSpawn,
            TombstonePrefab = GetEntity(authoring.TombstonePrefab)
        });
    }
}
