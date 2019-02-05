using UnityEngine;

public class BuffPathSpeed : Buff
{
    private PathMove pathMove;

    private void Awake()
    {
        pathMove = FindObjectOfType<LevelMaster>().levelStage.GetComponent<PathMove>();
        Destroy(gameObject, 5f);
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
        if (pathMove == null) return; 
            
        pathMove.moveInterval += 0.1f;
        FindObjectOfType<LevelMaster>().levelSpeed += 0.1f;
        buffBadge = Instantiate(buffBadgePrefab, Vector3.zero, Quaternion.identity, GameObject.FindWithTag("Respawn").transform);

    }

    public void BuffEnd()
    {
        if (pathMove == null) return;
        
        pathMove.moveInterval -= 0.1f;
        FindObjectOfType<LevelMaster>().levelSpeed -= 0.1f;
        Destroy(buffBadge);
    }
}