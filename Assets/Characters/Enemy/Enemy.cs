using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        Debug.Log("Take Damage from Enemy");
        PointsManager.instance.Bears++;

        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            other.GetComponent<Health>().Damage(damage);
        }
        
        // slow character for moment
        
        Destroy(gameObject);
    }
}