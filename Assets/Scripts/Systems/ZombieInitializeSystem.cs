using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct ZombieInitializeSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
    }
    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        //use idiomatic foreach to loop through every entities have the zombie tag
        foreach (var zombie in SystemAPI.Query<ZombieWalkAspect>().WithAll<ZombieTag>())
        {
            ecb.RemoveComponent<ZombieTag>(zombie.Entity);
            ecb.SetComponentEnabled<ZombieWalkingProperty>(zombie.Entity, false);
            ecb.SetComponentEnabled<ZombieEatProperty>(zombie.Entity, false);
        }
        ecb.Playback(state.EntityManager);
    }
}

