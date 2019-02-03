using UnityEngine;

public class Health : MonoBehaviour 
{
    public int maxHealth;
    [SerializeField] private int currentHealth;

    public int CurrentHealth
    {
        get
        {
            if (CompareTag("Player")) HeadsUpDisplay.instance.health.text = "Battle Scars \n" + currentHealth;
            
            return currentHealth;
        }

        set
        {
            currentHealth = value;
            if (CompareTag("Player")) HeadsUpDisplay.instance.health.text = "Battle Scars \n" + currentHealth;

            if (currentHealth <= 0)
            {
                Debug.Log("Dead");
                Destroy(gameObject);
            }
        }
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