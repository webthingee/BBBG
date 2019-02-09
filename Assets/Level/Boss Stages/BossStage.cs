using UnityEngine;

public class BossStage : MonoBehaviour
{
    public bool playerWins;
    
    private void Awake()
    {
        StopStage();    
    }

    protected void Update()
    {
        if (!playerWins) return;
        
        LevelMaster.instance.phaseBoss = false;
        LevelMaster.instance.phaseCompleteBoss = true;
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