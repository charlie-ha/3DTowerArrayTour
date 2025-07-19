using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject creditsPanel;//for main menu scene
    
    //for touchscreen scene
    public GameObject menuPanel;
    public Image menuPanelImage;
    public GameObject hamburgerButton; 
    public GameObject returnToMainMenu; 
    public GameObject settingsButton; 
    public GameObject closeButton; 
    public GameObject settingsPanel; 
    public GameObject soundManager;
    //main menu scene

    [Header("Sliders")]
    public Slider brightnessSlider;
    public Slider volumeSlider;

    [Header("Slider Text")]
    public Text brightnessText;
    public Text volumeText;

    [Header("Scene References")]
    public Light directionalLight; // Main directional light for brightness
    public AudioSource musicSource; // Background music AudioSource

    [Header("Settings")]
    public float maxBrightness = 5f; // Brightness multiplier
    public float maxVolume = 2f;     // Volume multiplier
    
    void Start()
    {
        //set up menu panel when start
        settingsPanel.SetActive(false);
        returnToMainMenu.SetActive(false);
        settingsButton.SetActive(false);
        closeButton.SetActive(false);
        menuPanelImage.color = new Color(0f,0f,0f,0f);

        // Initialize sliders if they are set
        if (brightnessSlider != null)
        {
            brightnessSlider.value = 0.5f;//starting brightness
            brightnessSlider.onValueChanged.AddListener(UpdateBrightness);
            UpdateBrightness(brightnessSlider.value);
        }

        if (volumeSlider != null)
        {
            volumeSlider.value = 0.2f;//starting volume
            volumeSlider.onValueChanged.AddListener(UpdateVolume);
            UpdateVolume(volumeSlider.value);
        }
    }

    // Called when Start Game button is pressed
    public void StartGame()
    {
        // Loads the next scene in Build Settings
        SceneManager.LoadScene(1);
    }

    // Called when Credits button is pressed
    public void ShowCredits()
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(true);
    }

    // Called when Back button on Credits panel is pressed
    public void HideCredits()
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(false);
    }

    // Called when Quit button is pressed
    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Shows in editor
        Application.Quit();     // Actually quits in build
    }

    ///////touchscreen scene////////
    public void OpenMenuPanel()//press hamburger menu button
    {
        menuPanelImage.color = new Color(0f,0f,0f,230/255f);
        hamburgerButton.SetActive(false);
        closeButton.SetActive(true);
        settingsButton.SetActive(true);
        returnToMainMenu.SetActive(true);

    }
    public void CloseMenuPanel()
    {
        menuPanelImage.color = new Color(0f,0f,0f,0f);
        hamburgerButton.SetActive(true);
        settingsPanel.SetActive(false);
        returnToMainMenu.SetActive(false);
        settingsButton.SetActive(false);
        closeButton.SetActive(false);
    }
    public void OpenSettingsPanel()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("cliked");
    }

    public void UpdateBrightness(float value)
    {
        float brightnessValue = Mathf.Lerp(0f, maxBrightness, value);
        if (directionalLight != null)
        {
            directionalLight.intensity = Mathf.Lerp(0f, maxBrightness, value);
            directionalLight.intensity = brightnessValue;
        }
        if (brightnessText != null)
            brightnessText.text = Mathf.RoundToInt(brightnessValue * 100f) + "%";
        
    }

    public void UpdateVolume(float value)
    {
        float volumeValue = Mathf.Lerp(0f, maxVolume, value);
        if (musicSource != null)
        {
            musicSource.volume = Mathf.Lerp(0f, maxVolume, value);
            musicSource.volume = volumeValue;
        }
        if (volumeText != null)
            volumeText.text = Mathf.RoundToInt(volumeValue * 100f) + "%";
    }
}
