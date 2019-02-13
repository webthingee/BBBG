using UnityEngine;

public class BuffSpeed : Buff
{
    private PlayerMove playerMove;

    private void Awake()
    {
        base.Awake();
        playerMove = GetComponentInParent<PlayerMove>();
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
        if (playerMove != null) playerMove.moveInterval -= 0.1f;
        buffBadge = Instantiate(buffBadgePrefab, Vector3.zero, Quaternion.identity, displayAt);

    }

    public void BuffEnd()
    {
        if (playerMove != null) playerMove.moveInterval += 0.1f;
        Destroy(buffBadge);
    }
}