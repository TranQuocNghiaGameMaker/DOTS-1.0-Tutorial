using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

[BurstCompile]
[UpdateAfter(typeof(SpawnZombieSystem))]
public partial struct ZombieRiseSystem : ISystem
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
        new ZombieRiseJob()
        {
            time = deltaTime
        }.ScheduleParallel();
    }
}

//IjobEntity is a way to iterate through numbers of entities with query, replacement for Entities.Foreach
[BurstCompile]
public partial struct ZombieRiseJob : IJobEntity
{
    public float time;
    [BurstCompile]
    private void Execute(ZombieRiseAspect zombie)
    {
        if (zombie.IsAboveGround)
        {
            zombie.SetGroundLevel();
            return;
        }
        zombie.Rise(time);
    }
}