using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;
    public AudioEvent coinAudioEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        Debug.Log("Add a Beet");
        coinAudioEvent.Play(SoundManager.instance.GetOpenAudioSource());
        PointsManager.instance.Coins++;
        
        Destroy(gameObject);
    }
}