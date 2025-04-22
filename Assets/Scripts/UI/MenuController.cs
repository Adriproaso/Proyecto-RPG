using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    GameObject buttons;
    [SerializeField]
    GameObject settingsHolder;

    [Header("Settings Tabs")]
    [SerializeField]
    GameObject soundSettings;
    [SerializeField]
    Button soundButton;
    [SerializeField]
    GameObject screenSettings;
    [SerializeField]
    Button screenButton;
    
    [Header("Settings References")]
    [SerializeField]
    TMP_Dropdown resolutionDropdown;
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    Slider sfxSlider;

    Resolution[] resolutions;

    #region MenuButtons
    public void ToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenMenu()
    {
        buttons.SetActive(false);
        settingsHolder.SetActive(true);
    }

    public void CloseMenu()
    {
        buttons.SetActive(true);
        settingsHolder.SetActive(false);
        OpenScreen();
    }

    public void ResetMenu()
    {
        soundSettings.SetActive(false);
        screenSettings.SetActive(false);
        soundButton.interactable = true;
        screenButton.interactable = true;
    }

    public void OpenSound()
    {
        ResetMenu();
        soundSettings.SetActive(true);
        soundButton.interactable = false;
    }

    public void OpenScreen()
    {
        ResetMenu();
        screenSettings.SetActive(true);
        screenButton.interactable = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ShowcaseScene");
    }

    public void ReturnButton()
    {
        ResetMenu();
        buttons.SetActive(true);
    }
    #endregion

    #region Settings Control

    private void Awake()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    /// <summary>
    /// Set video quality
    /// </summary>
    /// <param name="qualityIndex"></param>
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /// <summary>
    /// Set Fullscreen or not
    /// </summary>
    /// <param name="isFullscreen"></param>
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    /// <summary>
    /// Set Screen Resolution
    /// </summary>
    /// <param name="resolutionIndex"></param>
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
    }

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(musicSlider.value);
    }

    public void ToggleSFX()
    {
        AudioManager.instance.ToggleSFX();
    }

    public void SFXVolume()
    {
        AudioManager.instance.SFXVolume(sfxSlider.value);
    }

    #endregion
}
