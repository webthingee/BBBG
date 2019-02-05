using UnityEngine;

public class BuffSpeed : Buff
{
    private PlayerMove playerMove;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
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
        if (playerMove != null) playerMove.moveInterval -= 0.1f;
        buffBadge = Instantiate(buffBadgePrefab, Vector3.zero, Quaternion.identity, GameObject.FindWithTag("Respawn").transform);

    }

    public void BuffEnd()
    {
        if (playerMove != null) playerMove.moveInterval += 0.1f;
        Destroy(buffBadge);
    }
}