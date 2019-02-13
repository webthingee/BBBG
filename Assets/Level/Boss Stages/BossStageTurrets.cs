using UnityEngine;

public class BossStageTurrets : BossStage
{
    protected void Update()
    {
        if (GetComponentInChildren<ProjectileSpawner>().turretsList.Count <= 0)
        {
            playerWins = true;
        }
        
        base.Update();
    }
}