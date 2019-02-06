public class BossStageTurrets : BossStage
{
    private void Update()
    {
        if (GetComponentInChildren<ProjectileSpawner>().turretsList != null && GetComponentInChildren<ProjectileSpawner>().turretsList.Count <= 0)
        {
            playerWins = true;
        }
    }
}