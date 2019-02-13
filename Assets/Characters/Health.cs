using DG.Tweening;
using UnityEngine;

public class Health : MonoBehaviour 
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public AudioEvent damageAudioEvent;

    private HeartsDisplay hds;

    public int CurrentHealth
    {
        get
        {
            if (CompareTag("Player")) hds.UpdateHeartsDisplay(maxHealth, currentHealth); //HeadsUpDisplay.instance.health.text = "Heart Beats \n" + currentHealth;
            
            return currentHealth;
        }

        set
        {
            currentHealth = value;

            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }

            if (CompareTag("Player")) hds.UpdateHeartsDisplay(maxHealth, currentHealth); //HeadsUpDisplay.instance.health.text = "Heart Beats \n" + currentHealth;

            if (currentHealth <= 0)
            {
                Debug.Log("Dead");

                if (CompareTag("Player"))
                {
                    Debug.Log("player died");
                    LevelMaster.instance.PlayerDied();    
                }
                
                Destroy(gameObject);
            }
        }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
            CurrentHealth++;
        }
    }

    private void Awake()
    {
        hds = FindObjectOfType<HeartsDisplay>();
    }
    
    private void Start()
    {
        CurrentHealth = maxHealth;
        MaxHealth = maxHealth;
    }
	
    public void Damage(int damage = 1)
    {
        CurrentHealth -= damage;

        damageAudioEvent.Play(SoundManager.instance.GetOpenAudioSource());

        if (Camera.main == null) return;
        var mainCam = Camera.main;
        mainCam.transform.DOPunchPosition(new Vector3(0.2f, 0.2f), 0.1f);
        mainCam.transform.DOPunchRotation(new Vector3(0.2f, 0.2f), 0.1f);
    }

    public void Heal(int damage = 1)
    {
        CurrentHealth += damage;
    }
}