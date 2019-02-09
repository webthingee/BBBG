using UnityEngine;

public class BossStageBigSolo : BossStage
{
    private void Update()
    {
        base.Update();
        
        if (!GameObject.FindWithTag("Overlord"))
        {
            playerWins = true;
        }
    }
}