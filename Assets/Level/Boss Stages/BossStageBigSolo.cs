using UnityEngine;

public class BossStageBigSolo : BossStage
{
    private void Update()
    {
        if (!GameObject.FindWithTag("Overlord"))
        {
            playerWins = true;
        }
    }
}