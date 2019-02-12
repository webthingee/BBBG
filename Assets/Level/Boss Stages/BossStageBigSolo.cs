using UnityEngine;

public class BossStageBigSolo : BossStage
{
    public GameObject overlord;

    private void Start()
    {
        overlord = GameObject.FindWithTag("Overlord");
    }

    private void Update()
    {
        if (overlord == null)
        {
            playerWins = true;
        }
    
        base.Update();
    }
}