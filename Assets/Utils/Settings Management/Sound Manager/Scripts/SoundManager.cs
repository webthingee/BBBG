using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour 
{
    public static SoundManager instance;
    
    [Tooltip("Minimum number of Audio Sources to keep alive")]
    [Range(1,5)] public int minSources = 1;

    [Tooltip("Countdown after a played clip to execute a clean up of sources")]
    [Range(5,10)] public int cleanUpInterval = 5;

    [Range(0,1), SerializeField] private float musicVolume = 0.75f;
    [Range(0,1), SerializeField] private float sfxVolume = 0.75f;

    // Update anything live while changes occur
    public event Action OnMuiscVolumeChange = delegate { };
    
    public SoundManagerUI soundManagerUI;
    private bool soundManagerUIVisible;
    
    private List<AudioSource> audioSources = new List<AudioSource>();
    private bool hasScheduledCleanup;

    public float SfxVolume
    {
        get { return sfxVolume; }
        set { sfxVolume = MustBeBetween(value); }
    }

    public float MusicVolume
    {
        get { return musicVolume; }
        set { 
            musicVolume = MustBeBetween(value); 
            OnMuiscVolumeChange();
            
        }
    }

    void Awake ()
    {
        // Create as a singleton
        Singleton();
        
        // Populate the initial List<> of Audio Source Components
        BuildAudioSourcesList();
        
        // Get Volume
        GetMusicVolume();
        GetSfxVolume();
        
        // Get SoundManagerUI
//        var r = Resources.FindObjectsOfTypeAll<SoundManagerUI>();
//        soundManagerUI = r[0];
        
        if (soundManagerUI == null)
        {
            Debug.Log("No Audio Manager Available");
        }
        
        soundManagerUI.musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        soundManagerUI.sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    private void Update()
    {        
        soundManagerUI.musicVolumeSlider.value = musicVolume;
        soundManagerUI.sfxVolumeSlider.value = sfxVolume;
                
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
        {    
            soundManagerUIVisible = !soundManagerUIVisible;
            soundManagerUI.gameObject.SetActive(soundManagerUIVisible);
        }
    }
    
    public void SetMusicVolume(float value)
    {
        MusicVolume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void GetMusicVolume()
    {        
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume"); 
        }
        else
        {
            SetMusicVolume(0.5f);
        }
    }
    
    public void SetSfxVolume(float value)
    {
        SfxVolume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void GetSfxVolume()
    {        
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SfxVolume = PlayerPrefs.GetFloat("SFXVolume"); 
        }
        else
        {
            SetSfxVolume(0.5f);
        }
    }
    
    private float MustBeBetween(float value, float min = 0f, float max = 1f)
    {
        return value < min ? min : (value > max ? max : value);
    }
    
    /// Return first Audio Source that is not playing an audio clip
    public AudioSource GetOpenAudioSource ()
    {        
        int _audioSourcesTried = 0;

        if (audioSources.Count > 0)
        {
            if (!hasScheduledCleanup)
            {
                StartCoroutine(CleanUpAudioSources());
                hasScheduledCleanup = true;
            }

            foreach (AudioSource _audioSource in audioSources.ToArray())
            {
                _audioSourcesTried++;
                
                if (!_audioSource.isPlaying)
                {
                    return _audioSource;
                }

                if (_audioSourcesTried >= audioSources.Count)
                {
                    AudioSource newAudioSource = this.gameObject.AddComponent<AudioSource>();
                    newAudioSource.playOnAwake = false;
                    audioSources.Add(newAudioSource);
                    return newAudioSource;
                }
            }
        }
        // If we get here, we got a problem.
        Debug.LogError("Unable to acces or create AudioSource component");
        return null;
    }

    /// Builds the initial List<> of Audio Source Componenets in the GameObject
    void BuildAudioSourcesList ()
    {
        foreach (AudioSource _audioSource in GetComponents<AudioSource>())
        {
            audioSources.Add(_audioSource);
        }
    }

    /// Cleans the List of Audio Source components in the GameObject
    IEnumerator CleanUpAudioSources ()
    {
        yield return new WaitForSeconds(cleanUpInterval);
        
        int _audioSourcesToTry = minSources;
        
        foreach (AudioSource _audioSource in audioSources.ToArray())
        {
            if (!_audioSource.isPlaying && _audioSourcesToTry < audioSources.Count)
            {
                audioSources.Remove(_audioSource);
                Destroy(_audioSource);
            }
            _audioSourcesToTry++;
        }

        hasScheduledCleanup = false;
    }
    
    private void Singleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("not the instance " + name);
            Destroy(gameObject);
        }
    }
}