using UnityEngine;
using UnityEngine.UI;

public class SoundManagerUI : MonoBehaviour
{
    //#pragma warning disable    
    [Header("Sliders")]
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    //#pragma warning enable

    private SoundManager soundManager;

    private void Awake()
    {
        if (SoundManager.instance != null)
        {
            soundManager = SoundManager.instance;
        }
        else
        {
            Debug.Log("There is no SoundManager to control");
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
    
    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}