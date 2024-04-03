using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class VFXManager : MonoBehaviour
{

    [SerializeField] private GameObject smokeParticlesPrefab;


    private Dictionary<AllVfx, GameObject> vfxDictionary = new Dictionary<AllVfx, GameObject>();

    public static VFXManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        vfxDictionary[AllVfx.SmokeParticles] = smokeParticlesPrefab;
    }

    public void CreateVFXAtPoint(Vector3 position, AllVfx type)
    {
        Instantiate(vfxDictionary[type], position, quaternion.identity);
    }
    
    public void CreateVFXAtPoint(Vector3 position, AllVfx type, Transform parent)
    {
        Instantiate(vfxDictionary[type], position, quaternion.identity, parent);
    }

    public void CreateVFXAtPoint(Vector3 position, AllVfx type, Vector3 scale)
    {
        var go = Instantiate(vfxDictionary[type], position, quaternion.identity);
        go.transform.localScale = scale;
    }


}

public enum AllVfx
{
    SmokeParticles,
    FailedParticles
}
