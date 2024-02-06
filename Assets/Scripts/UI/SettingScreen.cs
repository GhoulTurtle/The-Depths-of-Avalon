using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingScreen : MonoBehaviour
{

    public static float masterVolume;
    public static float sfxVolume;
    public static float musicVolume;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public AudioMixer mainMixer;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("FirstTimeOpening") != null)
        {
         ResetVolumePrefs();
         UpdateMisxervolume();
            PlayerPrefs.SetInt("FirstTimeOpening", 1);
        }

        masterVolume = PlayerPrefs.GetFloat("MasterSliderValue");
        musicVolume = PlayerPrefs.GetFloat("MusicSliderValue");
        sfxVolume = PlayerPrefs.GetFloat("SFXSliderVaule");

        masterSlider.value = PlayerPrefs.GetFloat("MasterSliderValue");
        musicSlider.value = PlayerPrefs.GetFloat("MusicSliderValue");
        sfxSlider.value = sfxVolume = PlayerPrefs.GetFloat("SFXSliderVaule");
    }

    // Update is called once per frame
   void UpdateMisxervolume()
    {
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);

        PlayerPrefs.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);

        PlayerPrefs.Save();
    }

    void ResetVolumePrefs()
    {
        PlayerPrefs.SetFloat("MasterSliderValue", 1);
        PlayerPrefs.SetFloat("MusicSliderValue", 1);
        PlayerPrefs.SetFloat("SFXSliderValue", 1);

        masterVolume = PlayerPrefs.GetFloat("MasterSliderValue");
        musicVolume = PlayerPrefs.GetFloat("MusicSliderValue");
        sfxVolume = PlayerPrefs.GetFloat("SFXSliderValue");

        PlayerPrefs.Save();
    }
}
