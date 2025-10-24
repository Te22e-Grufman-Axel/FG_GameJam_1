using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Settings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResulutionIndex = 0;


    [SerializeField] private Toggle toggle;
    private bool fullscreen;

    [SerializeField] public Slider mainVolumeSlider;
    [SerializeField] public Slider musicVolumeSlider;
    [SerializeField] public Slider sfxVolumeSlider;
    [SerializeField] private SoundMixerManager soundMixerManager;

    private void Awake()
    {
        this.gameObject.SetActive(false);
        fullscreen = Screen.fullScreen;
        toggle.isOn = fullscreen;

        soundMixerManager = FindObjectOfType<SoundMixerManager>();
        soundMixerManager.settingsMenu = this;
    }

    [System.Obsolete]
    private void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + " Hz";
            options.Add(resolutionOption);

            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResulutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResulutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fullscreen);
    }

    public void ToggleFullscreen()
    {
        fullscreen = !Screen.fullScreen;
        Screen.fullScreen = fullscreen;
    }
}
