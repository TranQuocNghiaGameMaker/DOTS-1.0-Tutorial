using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

//Aspect groups Data Component so it can be easily accessed and modified 
public readonly partial struct BrainAspect : IAspect
{
    public readonly Entity entity;
    private readonly TransformAspect _transformAspect;
    private readonly RefRW<BrainHealth> _brainHealth;
    private readonly DynamicBuffer<BrainDamageBufferElement> _brainDamageBuffers;

    public void ReduceBrainHealth()
    {
        foreach (var brainDamageBuffer in _brainDamageBuffers)
        {
            _brainHealth.ValueRW.Value -= brainDamageBuffer.Value;
        }
        _brainDamageBuffers.Clear();
    }
}
