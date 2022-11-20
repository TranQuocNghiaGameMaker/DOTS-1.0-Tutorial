using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

[BurstCompile]
[UpdateAfter(typeof(ZombieRiseSystem))]
public partial struct ZombieWalkSystem : ISystem
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
        float deltaTime = SystemAPI.Time.DeltaTime;
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        //Get The Brain radius and offset
        var brainEntity = SystemAPI.GetSingletonEntity<BrainTag>();
        var brainScale = SystemAPI.GetComponent<LocalToWorldTransform>(brainEntity).Value.Scale;
        var brainRadius = brainScale * 5f + 0.5f; 
        new ZombieWalkJob()
        {
            ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
            deltaTime = deltaTime,
            brainRadius = brainRadius * brainRadius
        }.ScheduleParallel();
    }
}

//IjobEntity is a way to iterate through numbers of entities with query, replacement for Entities.Foreach
[BurstCompile]
public partial struct ZombieWalkJob : IJobEntity
{
    public float deltaTime;
    public float brainRadius;
    public EntityCommandBuffer.ParallelWriter ECB;
    [BurstCompile]
    private void Execute(ZombieWalkAspect zombie, [EntityInQueryIndex] int sortkey )
    {
        zombie.Walk(deltaTime);
        if (zombie.IsInStopRange(float3.zero, brainRadius))
        {
            ECB.RemoveComponent<ZombieWalkingProperty>(sortkey, zombie.Entity);
        }
    }
}