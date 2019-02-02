using UnityEngine;

public class BossStage : MonoBehaviour
{
    private void Awake()
    {
        GetComponentInChildren<TileRandomAppear>().enabled = false;
        GetComponentInChildren<ProjectileSpawner>().enabled = false;    
    }

    public void StartStage()
    {
        GetComponentInChildren<TileRandomAppear>().enabled = true;
        GetComponentInChildren<ProjectileSpawner>().enabled = true;
    }
}