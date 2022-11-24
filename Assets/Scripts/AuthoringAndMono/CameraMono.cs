using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMono : MonoBehaviour
{
    public static CameraMono Instance;

    [SerializeField] float _startRadius;
    [SerializeField] float _endRadius;
    [SerializeField] float _startHeight;
    [SerializeField] float _endHeight;
    [SerializeField] float _speed;

    public float RadiusAtScale(float scale) => Mathf.Lerp(_startRadius, _endRadius, 1 - scale);
    public float HeightAtScale(float scale) => Mathf.Lerp(_startHeight, _endHeight, 1 - scale);
    public float Speed => _speed;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

}
