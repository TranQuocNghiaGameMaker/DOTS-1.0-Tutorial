using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

[BurstCompile]
public partial struct SpawnZombieSystem : ISystem
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
        var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
        new SpawnZombieJob()
        {
            DeltaTime = deltaTime,
            ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
        }.Run();
    }
}

//IjobEntity is a way to iterate through numbers of entities with query, replacement for Entities.Foreach
[BurstCompile]
public partial struct SpawnZombieJob : IJobEntity
{
    public float DeltaTime;
    public EntityCommandBuffer ECB;
    [BurstCompile]
    private void Execute(GraveyardAspect graveyard)
    {
        graveyard.ZombieSpawnTimer -= DeltaTime;
        if (!graveyard.TimeToSpawn) return;
        if (graveyard.ZombieSpawnPoint.Length == 0) return;
        graveyard.ZombieSpawnTimer = graveyard.ZombieSpawnRate;
        var newZombieTransform = graveyard.GetZombieSpawnPoint();
        var newZombie = ECB.Instantiate(graveyard.ZombiePrefab);
        ECB.SetComponent(newZombie, new LocalToWorldTransform { Value = newZombieTransform });
        var zombieHeading = HelperFunction.GetHeading(newZombieTransform.Position, graveyard.Position);
        ECB.SetComponent(newZombie, new ZombieHeading
        {
            Value = zombieHeading
        });
    }
}