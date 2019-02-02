using UnityEngine;

public class Healing : MonoBehaviour
{
    public int healing = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        Debug.Log("Healing");
        other.GetComponent<Health>().Heal(healing);
        
        Destroy(gameObject);
    }
}