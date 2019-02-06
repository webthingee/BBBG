using UnityEngine;

public class BossStage : MonoBehaviour
{
    public bool playerWins;
    
    private void Awake()
    {
        StopStage();    
    }

    private void Update()
    {
        if (playerWins)
        {
            LevelMaster.instance.phaseBoss = false;
            LevelMaster.instance.phaseCompleteBoss = true;
        }

        if (GetComponentInChildren<ProjectileSpawner>().turretsList != null && GetComponentInChildren<ProjectileSpawner>().turretsList.Count <= 0)
        {
            playerWins = true;
        }
    }

    public void StartStage()
    {
        GetComponentInChildren<TileRandomAppear>().enabled = true;
        GetComponentInChildren<ProjectileSpawner>().enabled = true;
    }
    
    public void StopStage()
    {
        GetComponentInChildren<TileRandomAppear>().enabled = false;
        GetComponentInChildren<ProjectileSpawner>().enabled = false;    
    }
}