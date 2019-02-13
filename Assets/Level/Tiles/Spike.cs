using UnityEngine;

public class Spike : MonoBehaviour
{
    public AudioEvent spikeAudioEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        spikeAudioEvent.Play(SoundManager.instance.GetOpenAudioSource());
    }
}