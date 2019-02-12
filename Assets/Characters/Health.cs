using DG.Tweening;
using UnityEngine;

public class Health : MonoBehaviour 
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    //public AudioEvent damageAudioEvent;

    public FMOD.Studio.EventInstance audioAsset;
    [FMODUnity.EventRef] public string damageAudioEvent; //The string representing the path to the event in your banks
    FMOD.Studio.EventInstance HealthAudioEvent;

    public FMODUnity.StudioEventEmitter sm;
    
    
//    [FMODUnity.EventRef] public string _eventSound; 
//    FMOD.Studio.EventInstance _eventInstance; 
//    FMOD.Studio.ParameterInstance _myParameter; public string _parameter;

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

            if (currentHealth <= 2)
            {
                if (sm != null) sm.SetParameter("Lead_Line", 1);
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
//        if (_eventSound != null)
//        {
//            _eventInstance = FMODUnity.RuntimeManager.CreateInstance (_eventSound); 
//            _eventInstance.getParameter(_parameter, out _myParameter); 
//            _eventInstance.start ();
//        }
    }

//    void Update()
//    {
//        myParameter.setValue (currentHealth); // then select your event and write the name of your parameter in the inspector.
//    }
    
    private void Start()
    {
        CurrentHealth = maxHealth;
        MaxHealth = maxHealth;
    }
	
    public void Damage(int damage = 1)
    {
        CurrentHealth -= damage;

        //HealthAudioEvent = FMODUnity.RuntimeManager.CreateInstance(damageAudioEvent); //Create an instance of the event using the path name, and assign it to our variable
        //HealthAudioEvent.setParameterValue("Parameter1", 1);
        //HealthAudioEvent.start(); //Start the event 
        FMODUnity.RuntimeManager.PlayOneShot(damageAudioEvent);

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