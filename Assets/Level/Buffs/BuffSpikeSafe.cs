using UnityEngine;

public class BuffSpikeSafe : Buff
{    
    private void Awake()
    {
        Destroy(gameObject, 15f);
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
        buffBadge = Instantiate(buffBadgePrefab, Vector3.zero, Quaternion.identity, GameObject.FindWithTag("Respawn").transform);
    }

    public void BuffEnd()
    {
        Destroy(buffBadge);
    }
}
