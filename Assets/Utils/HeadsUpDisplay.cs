using TMPro;
using UnityEngine;

public class HeadsUpDisplay : MonoBehaviour
{
    public static HeadsUpDisplay instance;

    public TextMeshProUGUI health;
    public TextMeshProUGUI beets;
    public TextMeshProUGUI bears;
    
    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (instance == null) //Check if instance already exists
        {
            instance = this; //if not, set instance to this
            //DontDestroyOnLoad(gameObject); //Sets this to not be destroyed when reloading scene
        }
        else if (instance != this) //If instance already exists and it's not this:
        {
            Debug.Log("not the instance " + name);
            Destroy(gameObject); //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.

        }
    }
}