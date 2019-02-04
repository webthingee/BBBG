using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        Debug.Log("Add a Beet");
        PointsManager.instance.Coins++;
        
        Destroy(gameObject);
    }
}