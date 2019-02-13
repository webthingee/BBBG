using UnityEngine;

public class BuffPathSpeed : Buff
{
    private PathMove pathMove;

    private void Awake()
    {
        base.Awake();
        pathMove = FindObjectOfType<LevelMaster>().levelStage.GetComponent<PathMove>();
    }

    private void Start() // start ?
    {
        BuffStart();
    }

    private void OnDestroy() // destroy ?
    {
        BuffEnd();
    }

    public void BuffStart()
    {
        Destroy(gameObject, lengthToStay);
        
        if (pathMove == null) return; 
            
        pathMove.moveInterval += 0.1f;
        FindObjectOfType<LevelMaster>().levelSpeed += 0.1f;
        buffBadge = Instantiate(buffBadgePrefab, Vector3.zero, Quaternion.identity, displayAt);

    }

    public void BuffEnd()
    {
        if (pathMove == null) return;
        
        pathMove.moveInterval -= 0.1f;
        FindObjectOfType<LevelMaster>().levelSpeed -= 0.1f;
        Destroy(buffBadge);
    }
}