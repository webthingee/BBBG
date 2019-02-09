using UnityEngine;

public class BossStageTurrets : BossStage
{
    private void Update()
    {
        base.Update();

        if (GetComponentInChildren<ProjectileSpawner>().turretsList.Count <= 0)
        {
            playerWins = true;
        }
    }
}