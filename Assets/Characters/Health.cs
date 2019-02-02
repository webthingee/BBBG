using UnityEngine;

public class Health : MonoBehaviour 
{
    public int maxHealth;
    [SerializeField] private int currentHealth;

    public int CurrentHealth
    {
        get
        {
            HeadsUpDisplay.instance.health.text = "Battle Scars \n" + currentHealth;
            return currentHealth;
        }

        set
        {
            currentHealth = value;
            HeadsUpDisplay.instance.health.text = "Battle Scars \n" + currentHealth;

            if (currentHealth <= 0)
            {
                Debug.Log("Player Dead");
                Time.timeScale = 0;
            }
        }
    }

    private void Awake()
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