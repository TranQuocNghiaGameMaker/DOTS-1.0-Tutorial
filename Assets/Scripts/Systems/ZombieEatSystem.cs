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
        new ZombieEatJob()
        {
            deltaTime = deltaTime
        }.ScheduleParallel();
    }
}

//IjobEntity is a way to iterate through numbers of entities with query, replacement for Entities.Foreach
[BurstCompile]
public partial struct ZombieEatJob : IJobEntity
{
    public float deltaTime;
    [BurstCompile]
    private void Execute(ZombieEatAspect zombie, [EntityInQueryIndex] int sortkey )
    {
        zombie.Eat(deltaTime);
    }
}