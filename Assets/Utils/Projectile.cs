using UnityEngine;

public class Projectile : MonoBehaviour
{    
    public float moveSpeed = 1;

    private void Update()
    {
        transform.position = transform.position + Vector3.left * Time.deltaTime * moveSpeed;
    }
}