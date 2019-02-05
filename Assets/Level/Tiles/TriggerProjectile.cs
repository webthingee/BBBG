using UnityEngine;

public class TriggerProjectile : MonoBehaviour
{
    public GameObject prefabProjectile;
    public float moveSpeed = 1f;
    public Transform target;

    public void FireProjectile()
    {
        GameObject p = Instantiate(prefabProjectile, transform.position, Quaternion.identity);
        
        Projectile pObj = p.GetComponent<Projectile>();
        pObj.tag = "Player";
        pObj.Init(Vector3.right, target, moveSpeed);
        pObj.GetComponent<Damage>().componentName = "Turret";
    }
}