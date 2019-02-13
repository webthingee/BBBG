using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool canMove;
    public LayerMask blocker;
    
    public float moveInterval = 1f;
    public float nextMoveTime;

    public AudioEvent moveSound;

    public Vector2 moveDir;
    [HideInInspector] public bool doResetPlayerInput;
    
    private void Awake()
    {
        nextMoveTime = 0;
        moveDir = Vector2.zero;
    }

    private void Update()
    {
        if (!canMove) return;
        
        PlayerInput();
        
        if (Time.time > nextMoveTime)
        {
            nextMoveTime = Time.time + moveInterval;

            if (doResetPlayerInput)
            {
                moveDir = Vector2.zero;
                doResetPlayerInput = false;
                return;
            }

            if (moveDir != Vector2.zero)
            {
                if (moveSound) moveSound.Play(SoundManager.instance.GetOpenAudioSource());
                
                transform.position = transform.position + (Vector3)moveDir;
                moveDir = Vector2.zero;
            }
        }
    }

    private void PlayerInput()
    {
        float hMovement = Input.GetAxisRaw("Horizontal");
        float vMovement = Input.GetAxisRaw("Vertical");
                
        if (vMovement > 0.1f)
        {
            moveDir = Vector2.up;
        }
        
        if (vMovement < -0.1f)
        {
            moveDir = Vector2.down;
        }

        if (hMovement > 0.1f)
        {
            moveDir = Vector2.right;
        }

        if (hMovement < -0.1f)
        {
            moveDir = Vector2.left;
        }

        // reasons to set it back to zero
        
        // blocker layer object
        if (!CheckForOpenSpace(moveDir)) moveDir = Vector2.zero;

    }

    private bool CheckForOpenSpace(Vector2 dir)
    {
        return !Physics2D.Raycast(transform.position, dir, 1f, blocker);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)moveDir);
    }
}