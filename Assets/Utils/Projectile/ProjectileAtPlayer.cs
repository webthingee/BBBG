using UnityEngine;

public class ProjectileAtPlayer : MonoBehaviour
{    
    public float moveSpeed = 1;
    public AudioEvent damageAudioEvent;
    public bool targetInitialOnly;

    private PlayerMove playerMove;
    private Vector3 initialPos;

    private void Awake()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        
        if (playerMove != null) initialPos = playerMove.transform.position;
    }
    
    private void Update()
    {
        if (targetInitialOnly)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPos, Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, playerMove.transform.position, Time.deltaTime * moveSpeed);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        damageAudioEvent.Play(SoundManager.instance.GetOpenAudioSource());
    }
}