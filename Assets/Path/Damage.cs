using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        Debug.Log("Take Damage from " + name);
        other.GetComponent<Health>().Damage(damage);
        
        // slow or hold character for moment
        
        Destroy(gameObject);
    }
}