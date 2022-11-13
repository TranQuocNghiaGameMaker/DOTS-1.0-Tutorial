using Unity.Entities;
using Unity.Transforms;

public readonly partial struct GraveyardAspect : IAspect
{
    public readonly Entity Entity;

    private readonly TransformAspect _transfromAspect;

    private readonly RefRO<GraveyardProperties> _graveyardProperties;
    private readonly RefRW<GraveyardRandom> _graveyardRandom;   
}
