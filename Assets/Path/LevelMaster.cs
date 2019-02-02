using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    public static LevelMaster instance;

    public Transform level;
    public Transform bossStage;

    public bool phasePath;
    public bool phaseBoss;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        phasePath = true;
    }

    private void Update()
    {
        if (phasePath) MovePath();
    }

    private void MovePath()
    {        
        if (level.transform.position.x == -bossStage.transform.localPosition.x)
        {
            level.GetComponent<PathMove>().canMove = false;
            phasePath = false;
            phaseBoss = true;
            bossStage.GetComponent<BossStage>().StartStage();
        }
    }

    private void Singleton()
    {
        if (instance == null) //Check if instance already exists
        {
            instance = this; //if not, set instance to this
            DontDestroyOnLoad(gameObject); //Sets this to not be destroyed when reloading scene
        }
        else if (instance != this) //If instance already exists and it's not this:
        {
            Debug.Log("not the instance " + name);
            Destroy(gameObject); //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.

        }
    }
}