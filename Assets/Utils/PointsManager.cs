using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager instance;

    [SerializeField] private int coins;
    private int bears;

    public int Coins
    {
        get
        {
            HeadsUpDisplay.instance.beets.text = "Beets \n" + coins;
            return coins; 
        }
        set
        {
            coins = value;
            HeadsUpDisplay.instance.beets.text = "Beets \n" + coins;
        }
    }
    public int Bears
    {
        get
        {
            HeadsUpDisplay.instance.bears.text = "Bears Defeated \n" + bears;
            return bears; 
        }
        set
        {
            bears = value; 
            HeadsUpDisplay.instance.bears.text = "Bears Defeated \n" + bears;
        }
    }

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        Coins = coins;
        Bears = bears;
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