using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

[BurstCompile]
[UpdateAfter(typeof(ZombieWalkSystem))]
public partial struct ZombieEatSystem : ISystem
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
        var deltaTime = SystemAPI.Time.DeltaTime;
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        var brain = SystemAPI.GetSingletonEntity<BrainTag>();
        var brainScale = SystemAPI.GetComponent<LocalToWorldTransform>(brain).Value.Scale;
        var brainRadius = brainScale * 5f + 1;
        new ZombieEatJob()
        {
            brainRadius = brainRadius * brainRadius,
            deltaTime = deltaTime,
            ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
            brain = brain
        }.ScheduleParallel();
    }
}

//IjobEntity is a way to iterate through numbers of entities with query, replacement for Entities.Foreach
[BurstCompile]
public partial struct ZombieEatJob : IJobEntity
{
    public float deltaTime;
    public EntityCommandBuffer.ParallelWriter ECB;
    public Entity brain;
    public float brainRadius;
    [BurstCompile]
    private void Execute(ZombieEatAspect zombie, [EntityInQueryIndex] int sortkey )
    {
        if (zombie.isInEatingRange(float3.zero,brainRadius))
        {
            zombie.Eat(deltaTime,ECB,sortkey,brain);
        }
        else
        {
            ECB.SetComponentEnabled<ZombieEatProperty>(sortkey, zombie.Entity, false);
            ECB.SetComponentEnabled<ZombieWalkProperty>(sortkey, zombie.Entity, true);
        }
    }
}