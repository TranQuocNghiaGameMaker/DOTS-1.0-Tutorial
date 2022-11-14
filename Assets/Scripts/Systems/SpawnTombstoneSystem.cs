using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct SpawnTombstoneSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<GraveyardProperties>();
    }
    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        var graveyardEntity = SystemAPI.GetSingletonEntity<GraveyardProperties>();
        var graveyardAspect = SystemAPI.GetAspectRW<GraveyardAspect>(graveyardEntity);

        var ecb = new EntityCommandBuffer(Allocator.Temp);
        for (int i = 0; i < graveyardAspect.NumberTombstoneToSpawn; i++)
        {
            var tombstone = ecb.Instantiate(graveyardAspect.TombstonePrefab);
            var newTombstoneTransform = graveyardAspect.GetRandomTombstoneTransform();
            ecb.SetComponent(tombstone, new LocalToWorldTransform { Value = newTombstoneTransform });
        }
        ecb.Playback(state.EntityManager);
    }
}
