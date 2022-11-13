using Unity.Entities;
using Unity.Mathematics;

public struct GraveyardProperty : IComponentData
{
    public float2 FieldDimensions;
    public int NumberToSpawn;
    public Entity TombstonePrefab;
}
