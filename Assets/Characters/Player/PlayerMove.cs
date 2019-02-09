﻿using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool canMove;
    public LayerMask blocker;
    
    public float moveInterval = 0.5f;
    public float nextMoveTime;

    private Vector2 moveDir;
    private bool moveNow;
    
    private void Awake()
    {
        nextMoveTime = 0;
        moveDir = Vector2.zero;
    }

    private void Update()
    {
        if (!canMove) return;
        
        PlayerInput();

        if (moveNow)
        {
            moveNow = false;
            
            transform.position = transform.position + (Vector3)moveDir;
            moveDir = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (Time.time > nextMoveTime)
        {
            nextMoveTime = Time.time + moveInterval;
            moveNow = true;
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