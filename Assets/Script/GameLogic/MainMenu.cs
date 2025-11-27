using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    [Header("User actions input")]
    
    
    private Resolution[] resolutions;
    private int currentResolutionIndex = 0;
    public TMP_Text resText;
    public GameObject OptionPanel;
    public GameObject BaseMenuPanel;
    private void Awake()
    {
        
        // Charger les résolutions et trouver la résolution actuelle
        resolutions = GetAvailableResolutions();
        foreach (var res in resolutions)
        {
            Debug.Log(res.height +"X" + res.width);
        }
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
                break;
            }
        }

        Debug.Log("Résolution actuelle : " + GetCurrentResolutionString());
    }

    // Method to get available screen resolutions
    public Resolution[] GetAvailableResolutions()
    {
        return Screen.resolutions;
    }
    
    public string GetCurrentResolutionString()
    {
        var r = resolutions[currentResolutionIndex];
        resText.text = $"{r.width} x {r.height}";
        return $"{r.width} x {r.height}";
    }


    public void PressPlay()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("Levels Choice");
    }
    
    public void PressOption()
    {
        OptionPanel.SetActive(true);
        BaseMenuPanel.SetActive(false);
        Debug.Log("Option");
    }
    public void PressQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void PressReturn()
    { 
        OptionPanel.SetActive(false);
        BaseMenuPanel.SetActive(true);
    }


    public void ResolutionToUpper()
    {
        
        if (currentResolutionIndex < resolutions.Length - 1)
        {
            currentResolutionIndex++;
            ApplyResolution();
            Debug.Log("Resolution Up → " + GetCurrentResolutionString());
        }
    }
    
    public void ResolutionToLower()
    {
        if (currentResolutionIndex > 0)
        {
            currentResolutionIndex--;
            ApplyResolution();
            Debug.Log("Resolution Down → " + GetCurrentResolutionString());
        }
    }
    
    
    private void ApplyResolution()
    {
        Resolution r = resolutions[currentResolutionIndex];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);
    }

}
