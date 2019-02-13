using System.Net.Http.Headers;
using UnityEngine;

public enum DamageTypes
{
    None,
    Spike,
    Projectile
}

public class Damage : MonoBehaviour
{
    public int damage = 1;
    public string componentName = "PlayerMove";
    public DamageTypes damageType;    

    private void Awake()
    {
        componentName = "PlayerMove";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(transform.tag)) return;
        
        if (other.GetComponent(componentName) == null) return;

        if (damageType == DamageTypes.Spike && other.GetComponentInChildren<BuffSpikeSafe>() != null) return;
        
        Debug.Log(other.name + " was hit");
        
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.Damage(damage);
        }

        // slow or hold character for moment??

        Destroy(gameObject);
    }
}