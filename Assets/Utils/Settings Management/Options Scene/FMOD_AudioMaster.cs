using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class FMOD_AudioMaster : MonoBehaviour
{
    public static FMOD_AudioMaster instance;
    
    protected Bus master;
    protected Bus music;
    protected Bus sfx;

    [Range(0,1)] public float masterVolume = 1f;
    [Range(0,1)] public float musicVolume = 1f;
    [Range(0,1)] public float sfxVolume = 1f;

    protected string masterVolumePrefString = "FMOD_Master_Volume";
    protected string musicVolumePrefString = "FMOD_Music_Volume";
    protected string sfxVolumePrefString = "FMOD_SFX_Volume";

    private FMOD_AudioManager audioManager;
    private bool audioManagerVisible;
    
    private void Awake()
    {
        Singleton();

        var r = Resources.FindObjectsOfTypeAll<FMOD_AudioManager>();
        audioManager = r[0];
        
        if (audioManager == null)
        {
            Debug.Log("No Audio Manager Available");
            return;
        }
        
        master = RuntimeManager.GetBus("bus:/Master");
        music = RuntimeManager.GetBus("bus:/Master/Music");
        sfx = RuntimeManager.GetBus("bus:/Master/SFX");  
        
        if (PlayerPrefs.HasKey(masterVolumePrefString)) 
            masterVolume = PlayerPrefs.GetFloat(masterVolumePrefString);
        
        if (PlayerPrefs.HasKey(musicVolumePrefString))
            musicVolume = PlayerPrefs.GetFloat(musicVolumePrefString);
        
        if (PlayerPrefs.HasKey(sfxVolumePrefString))
            sfxVolume = PlayerPrefs.GetFloat(sfxVolumePrefString);
    }

    private void Update()
    {
        master.setVolume(masterVolume);
        PlayerPrefs.SetFloat(masterVolumePrefString, masterVolume);
        
        music.setVolume(musicVolume);
        PlayerPrefs.SetFloat(musicVolumePrefString, musicVolume);
        
        sfx.setVolume(sfxVolume);
        PlayerPrefs.SetFloat(sfxVolumePrefString, sfxVolume);
        
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
        {
            audioManagerVisible = !audioManagerVisible;
            audioManager.gameObject.SetActive(audioManagerVisible);
        }
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
    }
    
    public void SetSfxVolume(float value)
    {
        sfxVolume = value;
    }
    
    private void Singleton()
    {
        if (instance == null) //Check if instance already exists
        {
            instance = this; //if not, set instance to this
            // DontDestroyOnLoad(gameObject); //Sets this to not be destroyed when reloading scene
        }
        else if (instance != this) //If instance already exists and it's not this:
        {
            Debug.Log("not the instance " + name);
            Destroy(gameObject); //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.

        }
    }
}