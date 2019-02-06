using UnityEngine;

public class ProjectileAtPlayer : MonoBehaviour
{    
    public float moveSpeed = 1;

    public bool targetInitialOnly;

    private PlayerMove playerMove;
    private Vector3 initialPos;

    private void Awake()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        
        if (playerMove != null) initialPos = playerMove.transform.position;
    }
    
    private void Update()
    {
        if (targetInitialOnly)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPos, Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, playerMove.transform.position, Time.deltaTime * moveSpeed);
        }
    }
}