using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public static class HelperFunction 
{
    public static float GetHeading(float3 objectPosition,float3 targetPosition)
    {
        var x = objectPosition.x - targetPosition.x;
        var z = objectPosition.z - targetPosition.z;
        float result = math.atan2(x, z) + math.PI;
        return result;
    }
}
