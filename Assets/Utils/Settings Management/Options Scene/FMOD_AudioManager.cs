using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FMOD_AudioManager : MonoBehaviour
{
    #pragma warning disable    
    [Header("Sliders")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("Buttons")]
    [SerializeField] private Button gameStartButton;
    [SerializeField] private Button gameReturnButton;
    #pragma warning enable

    private FMOD_AudioMaster audioMaster;

    private void Awake()
    {
        if (FMOD_AudioMaster.instance != null)
        {
            audioMaster = FMOD_AudioMaster.instance;
        }
        else
        {
            Debug.Log("There is no FMOD_AudioMaster to control");
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
        masterVolumeSlider.value = FindObjectOfType<FMOD_AudioMaster>().masterVolume;
        masterVolumeSlider.onValueChanged.AddListener(FindObjectOfType<FMOD_AudioMaster>().SetMasterVolume);

        musicVolumeSlider.value = FindObjectOfType<FMOD_AudioMaster>().musicVolume;
        musicVolumeSlider.onValueChanged.AddListener(FindObjectOfType<FMOD_AudioMaster>().SetMusicVolume);

        sfxVolumeSlider.value = FindObjectOfType<FMOD_AudioMaster>().sfxVolume;
        sfxVolumeSlider.onValueChanged.AddListener(FindObjectOfType<FMOD_AudioMaster>().SetSfxVolume);
    }
    
    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}