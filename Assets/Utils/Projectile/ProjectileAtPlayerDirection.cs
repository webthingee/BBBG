using UnityEngine;

public class ProjectileAtPlayerDirection : MonoBehaviour
{    
    public float moveSpeed = 1;

    private PlayerMove playerMove;
    private Vector3 initialPos;

    private void Awake()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        
        if (playerMove != null) initialPos = moveDirection();
    }
    
    private void Update()
    {
        transform.position = transform.position + initialPos * Time.deltaTime * moveSpeed;
    }

    private Vector3 moveDirection()
    {
        Vector3 moveDir = playerMove.transform.position - transform.position;
        return moveDir.normalized;
    }
}