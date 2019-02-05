using UnityEngine;

public class TriggerStore : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        Debug.Log("Trigger Store");

        LevelMaster.instance.store.SetActive(true);
        Time.timeScale = 0;
        
        Destroy(gameObject);
    }
}