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
        new ZombieWalkJob()
        {
            deltatime = deltaTime
        }.ScheduleParallel();
    }
}

//IjobEntity is a way to iterate through numbers of entities with query, replacement for Entities.Foreach
[BurstCompile]
public partial struct ZombieWalkJob : IJobEntity
{
    public float deltatime;
    [BurstCompile]
    private void Execute(ZombieWalkAspect zombie )
    {
        zombie.Walk(deltatime);
    }
}