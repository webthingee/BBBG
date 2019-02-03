using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    public static LevelMaster instance;

    public GameObject prefabLevel;
    public GameObject prefabBossStage;
    
    private GameObject level;
    private GameObject bossStage;

    public bool phasePath;
    public bool phaseBoss;
    public bool phaseCompleteBoss;

    private void Awake()
    {
        Singleton();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
    }

    private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        level = Instantiate(prefabLevel, Vector3.zero, Quaternion.identity);
        bossStage = Instantiate(prefabBossStage, prefabBossStage.transform.position, Quaternion.identity, level.transform);
        
        phasePath = false;
        phaseBoss = false;
        phaseCompleteBoss = false;
        
        StartCoroutine(PhasesMaster());
    }

    IEnumerator PhasesMaster()
    {
        yield return StartCoroutine(SetupPhase());
        yield return StartCoroutine(PathPhase());
        yield return StartCoroutine(BossPhase());
        yield return StartCoroutine(BossCompletePhase());
        Debug.Log("Done");
    }

    IEnumerator SetupPhase()
    {
        yield return new WaitForSeconds(1f);
        
        Debug.Log("Start Setup");
        
        yield return new WaitForSeconds(3f);
        
        Debug.Log("Setting Up...");
        
        yield return new WaitForSeconds(2f);
        
        Debug.Log("Press AnyKey To Begin");

        while (!phasePath)
        {
            if (Input.anyKey) phasePath = true;
            yield return null;
        }
        
        Debug.Log("Complete Setup");

        for (int i = 3; i > 0; i--)
        {
            Debug.Log(i.ToString());
            yield return new WaitForSeconds(1f);
        }
        
        Debug.Log("GO!");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator PathPhase()
    {
        Debug.Log("Start PathPhase");
        
        FindObjectOfType<PlayerMove>().canMove = true;
        level.GetComponent<PathMove>().canMove = true;
        
        EnemyMove[] enemies = FindObjectsOfType<EnemyMove>();
        foreach (EnemyMove enemy in enemies)
        {
            enemy.canMove = true;
        }

        while (phasePath)
        {
            MovePath();
            yield return null;
        }        
        
        level.GetComponent<PathMove>().canMove = false;

        Debug.Log("Complete PathPhase");
    }
    
    IEnumerator BossPhase()
    {
        Debug.Log("Start BossPhase");
        
        bossStage.GetComponent<BossStage>().StartStage();

        while (phaseBoss)
        {
            yield return null;
        }
        
        Debug.Log("Complete BossPhase");
    }
    
    IEnumerator BossCompletePhase()
    {
        Debug.Log("Start BossCompletePhase");
        
        bossStage.GetComponent<BossStage>().StopStage();

        yield return new WaitForSeconds(1f);
        
        Debug.Log("Player Wins!");

        while (phaseCompleteBoss)
        {            
            if (Input.anyKey) StartOver();
            yield return null;
        }
        
        Debug.Log("Complete BossCompletePhase");
    }
    
    private void MovePath()
    {
        if (level.transform.position.x != -bossStage.transform.localPosition.x) return;
        
        phasePath = false;
        phaseBoss = true;
    }

    private void StartOver()
    {
        Debug.Log("Start Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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