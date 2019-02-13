using UnityEngine;

public class BuffSpikeSafe : Buff
{
    private void Awake()
    {
        base.Awake();
        Destroy(gameObject, lengthToStay);
    }

    private void Start()
    {
        BuffStart();
    }

    private void OnDestroy()
    {
        BuffEnd();
    }

    public void BuffStart()
    {
        buffBadge = Instantiate(buffBadgePrefab, Vector3.zero, Quaternion.identity, displayAt);
    }

    public void BuffEnd()
    {
        Destroy(buffBadge);
    }
}