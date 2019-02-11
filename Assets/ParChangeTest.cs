using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class ParChangeTest : MonoBehaviour
{
    private Bus Master;

    [Range(0,1)] public float masterVolume = 1f;
    
    private void Awake()
    {
        Master = RuntimeManager.GetBus("bus:/Master");
    }

    private void Update()
    {
        Master.setVolume(masterVolume);
    }
}
