using UnityEngine;

public class BuffHealthPlus : Buff
{
    private PlayerMove playerMove;

    private void Awake()
    {
        base.Awake();
        playerMove = GetComponentInParent<PlayerMove>();
        Destroy(gameObject, 1f);
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
        if (playerMove != null) playerMove.GetComponent<Health>().MaxHealth++;
        buffBadge = Instantiate(buffBadgePrefab, Vector3.zero, Quaternion.identity, displayAt);
    }

    public void BuffEnd()
    {
        Destroy(buffBadge);
    }
}