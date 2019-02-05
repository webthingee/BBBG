using UnityEngine;

public class Projectile : MonoBehaviour
{    
    public float moveSpeed = 1;
    public Vector3 direction = Vector3.left;

    public Transform target;

    public void Init(Vector3 direction, Transform target = null, float moveSpeed = 1f)
    {
        this.moveSpeed = moveSpeed;
        this.target = target;
        this.direction = direction;
    }
    
    private void Update()
    {
        if (!target)
        {
            transform.position = transform.position + direction * Time.deltaTime * moveSpeed;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
        }
    }
}