using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class LevelMaster : MonoBehaviour
{
    public static LevelMaster instance;

    [Header("Level")]
    public int level;
    public bool doNotLoadLevel;
    
    [Header("Level Stage")]
    public GameObject prefabLevel;
    public float levelSpeed;
    public int[] columns;
    public int[] initialOpen;
    public int[] endOpen;
    
    [Header("Boss Stage")]
    public GameObject[] prefabBossStage;

    [Header("Overlay")]
    public GameObject overlay;
    public TextMeshProUGUI overlayText;

    [HideInInspector] public bool phasePath;
    [HideInInspector] public bool phaseBoss;
    [HideInInspector] public bool phaseCompleteBoss;
    
    private GameObject levelStage;
    private GameObject bossStage;
    private PlayerMove playerMove;
    private bool isPlayerDead;
    private bool hasPlayerWon;

    private void Awake()
    {
        Singleton();
        
        playerMove = FindObjectOfType<PlayerMove>();

        //level = 0;
    }

    private void Start()
    {
        level++;
        LoadNewLevel();
    }

    IEnumerator PhasesMaster()
    {
        phasePath = false;
        overlay.SetActive(true);
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
        overlayText.text = "Start Setup";
        
        yield return new WaitForSeconds(1f);
        
        Debug.Log("Setting Up...");
        overlayText.text = "Setting Up...";
        
        yield return new WaitForSeconds(1f);
        
        Debug.Log("Press AnyKey To Begin");
        overlayText.text = "Press AnyKey To Begin";

        while (!phasePath)
        {
            if (Input.anyKey) phasePath = true;
            yield return null;
        }
        
        Debug.Log("Complete Setup");
        overlayText.text = "Complete Setup";
        
        for (int i = 3; i > 0; i--)
        {
            Debug.Log(i.ToString());
            overlayText.text += " " + i;
            
            yield return new WaitForSeconds(1f);
        }
        
        Debug.Log("GO!");
        overlayText.text = "GO!";
        
        yield return new WaitForSeconds(1f);

        overlay.SetActive(false);

        yield return new WaitForSeconds(1f);
    }

    IEnumerator PathPhase()
    {
        Debug.Log("Start PathPhase");
        
        playerMove.canMove = true;
        if (levelStage) levelStage.GetComponent<PathMove>().canMove = true;
        
        EnemyMove[] enemies = FindObjectsOfType<EnemyMove>();
        foreach (EnemyMove enemy in enemies)
        {
            enemy.canMove = true;
        }

        while (phasePath)
        {
            if (levelStage) MovePath();
            yield return null;
        }        
        
        if (levelStage) levelStage.GetComponent<PathMove>().canMove = false;

        Debug.Log("Complete PathPhase");
    }
    
    IEnumerator BossPhase()
    {
        Debug.Log("Start BossPhase");
        
        if (bossStage) bossStage.GetComponent<BossStage>().StartStage();

        while (phaseBoss)
        {
            yield return null;
        }
        
        Debug.Log("Complete BossPhase");
    }
    
    IEnumerator BossCompletePhase()
    {
        Debug.Log("Start BossCompletePhase");
        
        if (bossStage) bossStage.GetComponent<BossStage>().StopStage();

        yield return new WaitForSeconds(1f);
        
        overlay.SetActive(true);
        
        Debug.Log("Player Wins! Any button to continue to next farm");
        overlayText.text = "Cylon Overload Destroyed! Press Any Key To Continue...";
        
        
        while (phaseCompleteBoss)
        {
            if (Input.anyKey) NextLevel();
            yield return null;
        }
    }
    
    private void MovePath()
    {
        if (levelStage.transform.position.x != -bossStage.transform.localPosition.x) return;
        
        phasePath = false;
        phaseBoss = true;
    }

    private void NextLevel()
    {
        level++; // second appearance - improve?
        
        phaseCompleteBoss = false;
        
        Debug.Log("Loading Next Level");

        if (level >= prefabBossStage.Length)
        {
            PlayerWins();
        }
        else
        {
            LoadNewLevel();
        }
    }
    
    private void StartNewGame()
    {
        StopAllCoroutines();
        
        isPlayerDead = false;
        
        Debug.Log("Start New Game");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }

    private void Singleton()
    {
        if (instance == null) //Check if instance already exists
        {
            instance = this; //if not, set instance to this
        }
        else if (instance != this) //If instance already exists and it's not this:
        {
            Debug.Log("not the instance " + name);
            Destroy(gameObject); //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
        }
    }
    
    private void LoadNewLevel()
    {
        phasePath = false;
        phaseBoss = false;
        phaseCompleteBoss = false;

        int levelIndex = level - 1;
                
        playerMove.transform.parent = null;
        playerMove.canMove = false;

        if (levelStage) Destroy(levelStage.gameObject);
        if (bossStage) Destroy(bossStage.gameObject);
        
        if (!doNotLoadLevel)
        {
            levelStage = Instantiate(prefabLevel, Vector3.zero, Quaternion.identity);
            levelStage.GetComponent<PathMove>().moveInterval = levelSpeed;
            levelStage.GetComponent<PathMove>().canMove = false;
            PathColumnsGenerator generator = levelStage.GetComponentInChildren<PathColumnsGenerator>();
            generator.columns = columns[levelIndex];
            generator.initialOpen = initialOpen[levelIndex];
            generator.endOpen = endOpen[levelIndex];
            
            bossStage = Instantiate(prefabBossStage[levelIndex], prefabBossStage[levelIndex].transform.position, Quaternion.identity, levelStage.transform);
            Vector3 pos = bossStage.transform.position;
            pos.x = columns[levelIndex];
            bossStage.transform.position = pos;
        }
        
        playerMove.transform.position = new Vector3(-10, 0, 0);
        
        StartCoroutine(PhasesMaster());
    }

    public void PlayerDied()
    {
        StopAllCoroutines();
        
        playerMove.canMove = false;
        levelStage.GetComponent<PathMove>().canMove = false;

        isPlayerDead = true;
        StartCoroutine(AfterPlayerDied());
    }

    IEnumerator AfterPlayerDied()
    {
        overlay.SetActive(true);
        
        Debug.Log("Player Died!");
        overlayText.text = "Player Died";;

        yield return new WaitForSeconds(1f);
        
        overlay.SetActive(true);
        
        overlayText.text += "\n Press Any Key To Continue...";
        
        while (isPlayerDead)
        {
            if (Input.anyKey) StartNewGame();
            yield return null;
        }
    }
    
    public void PlayerWins()
    {
        StopAllCoroutines();
        
        playerMove.canMove = false;
        levelStage.GetComponent<PathMove>().canMove = false;

        hasPlayerWon = true;
        StartCoroutine(PlayerHasWon());
    }
    
    IEnumerator PlayerHasWon()
    {
        overlay.SetActive(true);
        
        Debug.Log("Player Wins!");
        overlayText.text = "Player Wins";;

        yield return new WaitForSeconds(5f);
                
        overlayText.text += "\n Press Any Key To Continue...";
        
        while (hasPlayerWon)
        {
            if (Input.anyKey) StartNewGame();
            yield return null;
        }
    }
}