using UnityEngine;
using DG.Tweening;

public class PathMove : MonoBehaviour
{
    public float moveInterval = 1f;
    public bool canMove;
    
    private float nextMoveTime;
    
    private void Awake()
    {
        // set the time interval
        nextMoveTime = moveInterval;
        
        // bring the player into the container so it moves too
        FindObjectOfType<PlayerMove>().transform.parent = transform;
    }

    private void Update()
    {
        if (!canMove) return;
        if (Time.time < nextMoveTime) return;

        MovePath();
        
        nextMoveTime = Time.time + moveInterval;
        
        //transform.position = transform.position + Vector3.left * 0.5f;
    }

    private void MovePath()
    {
        transform.DOMoveX(transform.position.x - 0.5f, moveInterval * 0.75f);
    }
}