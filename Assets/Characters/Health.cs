using UnityEngine;

public class Health : MonoBehaviour 
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

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
    }
	
    public void Damage(int damage = 1)
    {
        CurrentHealth -= damage;
    }

    public void Heal(int damage = 1)
    {
        CurrentHealth += damage;
    }
}