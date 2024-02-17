//Last Editor: Matt Santos
//Last Edited: Feb 15

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.UIElements.Experimental;

public class SettingScreen : MonoBehaviour
{
    [Header("Resolution")]
     public List<ResItem> resolutions = new List<ResItem>();

     private int selectedRes;
     
     public Text resolutionLabel;
      
      public Toggle fullscreeenTog;
    [Header("Audio")]
    public AudioMixer mainMixer;
    public static float masterVolume;
    public static float sfxVolume;
    public static float musicVolume;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

   
    // Start is called before the first frame update
    void Awake()
    {
        if (!PlayerPrefs.HasKey("FirstTimeOpening"))

        {
            ResetVolumePrefs();
            UpdateMixerVolume();
            PlayerPrefs.SetInt("FirstTimeOpening", 1);
        }

        masterVolume = PlayerPrefs.GetFloat("MasterSliderValue");
        musicVolume = PlayerPrefs.GetFloat("MusicSliderValue");
        sfxVolume = PlayerPrefs.GetFloat("SFXSliderVaule");

        masterSlider.value = PlayerPrefs.GetFloat("MasterSliderValue");
        musicSlider.value = PlayerPrefs.GetFloat("MusicSliderValue");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXSliderValue");

        bool foundRes = false;
        for(int i = 0; i<resolutions.Count;i++)
        {
            if(Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedRes = i;
                UpdateResLabel();
            }
        }
        if(!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            selectedRes = resolutions.Count -1;
            

        }
    }

    //Update is called once per frame
  

    
    public void  ChangeMasterVolume(float value)
    {
     masterVolume = value;
     UpdateMixerVolume();
    }

    public void ChangeMusicVolume(float value)
    {
     musicVolume = value;
     UpdateMixerVolume();

    }

    public void ChangeSFXVolume(float value)
    {
      sfxVolume = value;
      UpdateMixerVolume();
    } 
    void UpdateMixerVolume()
    {
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20 );
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);

        PlayerPrefs.SetFloat("MasterVolume", Mathf.Log10(masterVolume));
        PlayerPrefs.SetFloat("SFXVolume", Mathf.Log10(sfxVolume));
        PlayerPrefs.SetFloat("MusicVolume", Mathf.Log10(musicVolume));

        PlayerPrefs.Save();
        Debug.Log("MixerUpdated");
        
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
    
    public void ResLeft()
    {
        selectedRes--;
        if(selectedRes < 0)
        {
            selectedRes = 0;
        }
            UpdateResLabel();

    }

    public void ResRight()
    {
        selectedRes++;
        if(selectedRes > resolutions.Count - 1)
        {
            selectedRes = resolutions.Count - 1;
        }
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedRes].horizontal.ToString() + "X" + resolutions[selectedRes].vertical.ToString();
    }
    public void Apply()
    {
        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, fullscreeenTog.isOn);
    }
    [System.Serializable]
    public class ResItem
    {
        public int horizontal,vertical;
    }
}
