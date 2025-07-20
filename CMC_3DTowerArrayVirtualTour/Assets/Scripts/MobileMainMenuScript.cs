using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MobileMainMenuScript : MonoBehaviour
{
    public GameObject creditsPanel;//for main menu scene
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
}
