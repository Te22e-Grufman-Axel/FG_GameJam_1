using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    // [SerializeField] private Slider mainVolumeSlider;
    // [SerializeField] private Slider musicVolumeSlider;
    // [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] public Settings settingsMenu;

    // private void Start()
    // {
    //     mainVolumeSlider = settingsMenu.mainVolumeSlider;
    //     musicVolumeSlider = settingsMenu.musicVolumeSlider;
    //     sfxVolumeSlider = settingsMenu.sfxVolumeSlider;
    // }
    // private void OnActivate()
    // {
    //     mainVolumeSlider = settingsMenu.mainVolumeSlider;
    //     musicVolumeSlider = settingsMenu.musicVolumeSlider;
    //     sfxVolumeSlider = settingsMenu.sfxVolumeSlider;
    // }
    private void Update()
    {
       if(settingsMenu.mainVolumeSlider != null) SetMasterVolume(settingsMenu.mainVolumeSlider.value);
       if(settingsMenu.musicVolumeSlider != null) SetMusicVolume(settingsMenu.musicVolumeSlider.value);
       if(settingsMenu.sfxVolumeSlider != null) SetSFXVolume(settingsMenu.sfxVolumeSlider.value);
    }


    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SoundFxVolume", Mathf.Log10(volume) * 20);
    }

}
