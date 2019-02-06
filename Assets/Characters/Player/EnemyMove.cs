using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool canMove;
    public bool verticalOnly;
    public MinMax moveInterval;
    public LayerMask blocker;
    
    private float nextMoveTime;
    private Vector2 moveDir;
    
    private void Awake()
    {
        nextMoveTime = Random.Range(moveInterval.minValue, moveInterval.maxValue);
        moveDir = Vector2.zero;
    }

    private void Update()
    {
        if (!canMove) return;
        
        PlayerInput();

        if (Time.time > nextMoveTime)
        {            
            transform.position = transform.position + (Vector3)moveDir;
            nextMoveTime = Time.time + Random.Range(moveInterval.minValue, moveInterval.maxValue);
            moveDir = Vector2.zero;
        }
    }

    private void PlayerInput()
    {
        int randDir = Random.Range(1, 5);
        
        if (verticalOnly) randDir = Random.Range(1, 3);
                
        if (randDir == 1)
        {
            moveDir = Vector2.up;
        }
        
        if (randDir == 2)
        {
            moveDir = Vector2.down;
        }

        if (randDir == 3)
        {
            moveDir = Vector2.right;
        }

        if (randDir == 4)
        {
            moveDir = Vector2.left;
        }

        // reasons to set it back to zero
        
        // blocker layer object
        if (!CheckForOpenSpace(moveDir)) moveDir = Vector2.zero;

    }

    private bool CheckForOpenSpace(Vector2 dir)
    {
        return !Physics2D.Raycast(transform.position + (Vector3)dir, dir, 1f, blocker);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + (Vector3)moveDir * .75f, transform.position + (Vector3)moveDir * 1.25f);
    }
}