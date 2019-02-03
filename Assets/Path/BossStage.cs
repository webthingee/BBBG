using UnityEngine;

public class BossStage : MonoBehaviour
{
    public bool playerWins;
    //public bool bossWins;
    
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