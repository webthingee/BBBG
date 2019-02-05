using UnityEngine;

public class Trigger : MonoBehaviour
{
    private TriggerProjectile triggerProjectile;

    private void Awake()
    {
        triggerProjectile = GetComponent<TriggerProjectile>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        Debug.Log("Trigger");

        if (triggerProjectile != null)
        {
            triggerProjectile.FireProjectile();    
        }
        
        Destroy(gameObject);
    }
}