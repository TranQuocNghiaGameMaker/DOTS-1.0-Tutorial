using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

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

        var spawnPoints = new NativeList<float3>(Allocator.Temp);
        var tombstoneOffset = new float3(0f, -2, 1);
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        for (int i = 0; i < graveyardAspect.NumberTombstoneToSpawn; i++)
        {
            var tombstone = ecb.Instantiate(graveyardAspect.TombstonePrefab);
            var newTombstoneTransform = graveyardAspect.GetRandomTombstoneTransform();
            ecb.SetComponent(tombstone, new LocalToWorldTransform { Value = newTombstoneTransform });
            var newZombieSpawnPoint = newTombstoneTransform.Position + tombstoneOffset;
            spawnPoints.Add(newZombieSpawnPoint);
        }
        graveyardAspect.ZombieSpawnPoint = spawnPoints.ToArray(Allocator.Persistent);
        ecb.Playback(state.EntityManager);
    }
}